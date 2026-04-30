using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RPNEvaluator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Image level_selector;
    public GameObject button;
    public GameObject enemy;
    public SpawnPoint[] SpawnPoints;    
    public List<Enemy> enemies;
    public List<PlayerClass> classes;
    public List<Levels> levels;
    public string current_level;
    public int current_wave = 1;
    public int max_waves;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Enemies deserialized
        string jsonString1 = File.ReadAllText(Application.dataPath + "/Resources/enemies.json");
        enemies = JsonConvert.DeserializeObject<List<Enemy>>(jsonString1);
        
        // Classes deserialized
        string jsonString2 = File.ReadAllText(Application.dataPath + "/Resources/classes.json");
        var classDictionary = JsonConvert.DeserializeObject<Dictionary<string, PlayerClass>>(jsonString2);
        classes = classDictionary.Values.ToList();

        // Levels deserialized
        string jsonString3 = File.ReadAllText(Application.dataPath + "/Resources/levels.json");
        levels = JsonConvert.DeserializeObject<List<Levels>>(jsonString3);

        // Adds in buttons to select difficulty
        for (int i = 0; i < levels.Count; i++)
        {
            GameObject difficulty_selector = Instantiate(button, level_selector.transform);
            difficulty_selector.transform.localPosition = new Vector3(0, 130 - (i + 1) * 60);
            difficulty_selector.GetComponent<MenuSelectorController>().spawner = this;
            difficulty_selector.GetComponent<MenuSelectorController>().SetLevel(levels[i].name);
        }
    }

    void ResetGame()
    {
        GameManager.Instance.ClearEnemies();

        current_wave = 1;
        current_level = "";
        max_waves = 0;
        GameManager.Instance.state = GameManager.GameState.PREGAME;
        GameManager.Instance.wave_time = 0;
        GameManager.Instance.countdown = 0;

        StopAllCoroutines();

        level_selector.gameObject.SetActive(true);
    }

    // Counts how long the player has been in the wave for
    void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.INWAVE)
        {
            GameManager.Instance.wave_time += Time.deltaTime;
        }
        // Level end
        else if (GameManager.Instance.state == GameManager.GameState.WAVEEND && current_wave >= max_waves)
        {
            GameManager.Instance.state = GameManager.GameState.GAMEWON;
        }
    }

    public void StartLevel(string levelname)
    {
        current_level = levelname;
        // Checks each level for the wave count and defaults to 1000 for endless
        max_waves = levels.FirstOrDefault(l => l.name == levelname)?.waves ?? 1000;

        level_selector.gameObject.SetActive(false);
        // this is not nice: we should not have to be required to tell the player directly that the level is starting
        GameManager.Instance.player.GetComponent<PlayerController>().StartLevel();
        StartCoroutine(SpawnWave());
    }

    public void NextWave()
    {
        if (GameManager.Instance.state == GameManager.GameState.GAMEWON || GameManager.Instance.state == GameManager.GameState.GAMEOVER)
        {
            ResetGame();
            return;
        }
        GameManager.Instance.wave_time = 0;
        current_wave++;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        GameManager.Instance.state = GameManager.GameState.COUNTDOWN;
        GameManager.Instance.countdown = 3;
        for (int i = 3; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.countdown--;
        }
        GameManager.Instance.state = GameManager.GameState.INWAVE;
       
        Levels level = levels.Find(t => t.name == current_level);
        foreach (SpawnList spawnList in level.spawns) 
        {
            StartCoroutine(SpawnEnemies(spawnList));
        }

        yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
        GameManager.Instance.state = GameManager.GameState.WAVEEND;
    }

    IEnumerator SpawnEnemies(SpawnList spawnList)
    {
        Enemy enemy = enemies.Find(t => t.name == spawnList.enemy);
        var dict = new Dictionary<string, int> { {"wave", current_wave}};
        int count = RPNEvaluator.RPNEvaluator.Evaluate(spawnList.count, dict);
        int hp = enemy.hp;
        if (!string.IsNullOrWhiteSpace(spawnList.hp) && spawnList.hp != "null")
        {
            dict["base"] = enemy.hp;
            hp = RPNEvaluator.RPNEvaluator.Evaluate(spawnList.hp, dict);
        }

        if (hp <= 0)
        {
            hp = enemy.hp > 0 ? enemy.hp : 1; 
        }

        int damage = enemy.damage;
        if (!string.IsNullOrEmpty(spawnList.damage))
        {
            dict["base"] = enemy.damage;
            damage = RPNEvaluator.RPNEvaluator.Evaluate(spawnList.damage, dict);
        }
        if (damage < 0) damage = 0;

        int speed = enemy.speed;
        if (!string.IsNullOrEmpty(spawnList.speed))
        {
            dict["base"] = enemy.speed;
            speed = RPNEvaluator.RPNEvaluator.Evaluate(spawnList.speed, dict);
        }
        if (speed <= 0) speed = enemy.speed > 0 ? enemy.speed : 1;

        Debug.Log($"count: {count} hp: {hp} damage: {damage} speed {speed}");
        int total = 0;
        while (total < count)
        {
            foreach (int seqNum in spawnList.sequence)
            {
                for (int i = 0; (i < seqNum) && (total < count); i++)
                {
                    StartCoroutine(SpawnEnemy(enemy,hp,damage,speed));
                    total++;
                }
                float delay = !string.IsNullOrEmpty(spawnList.delay) ? float.Parse(spawnList.delay) : GameManager.Instance.countdown;
                yield return new WaitForSeconds(delay);
            }
        }
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator SpawnEnemy(Enemy data, int hp, int damage, int speed)
    {
        SpawnPoint spawn_point = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)];
        Vector2 offset = UnityEngine.Random.insideUnitCircle * 1.8f;
        
        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject new_enemy = Instantiate(enemy, initial_position, Quaternion.identity);

        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(data.sprite);
        EnemyController en = new_enemy.GetComponent<EnemyController>();
        en.hp = new Hittable(hp, Hittable.Team.MONSTERS, new_enemy);
        en.speed = speed;
        GameManager.Instance.AddEnemy(new_enemy);
        yield return new WaitForSeconds(0.5f);
    }
}
