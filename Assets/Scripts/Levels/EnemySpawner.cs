using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using Microsoft.VisualBasic;
using System.ComponentModel;

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
    public int current_wave;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject selector = Instantiate(button, level_selector.transform);
        selector.transform.localPosition = new Vector3(0, 130);
        selector.GetComponent<MenuSelectorController>().spawner = this;
        selector.GetComponent<MenuSelectorController>().SetLevel("Start");

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

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.enemy_count == 0 && GameManager.Instance.state == GameManager.GameState.INWAVE)
        {
            GameManager.Instance.state = GameManager.GameState.WAVEEND;
        }
    }

    public void StartLevel(string levelname)
    {
        current_wave = 1;
        level_selector.gameObject.SetActive(false);
        // this is not nice: we should not have to be required to tell the player directly that the level is starting
        GameManager.Instance.player.GetComponent<PlayerController>().StartLevel();
        StartCoroutine(SpawnWave());
    }

    public void NextWave()
    {
        // Level end
        if (current_wave >= 10)
        {
            return;
        }
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
        
        for (int i = 0; i < current_wave; i++)
        {
            StartCoroutine(SpawnEnemy(enemies[1]));
        }

        yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
        GameManager.Instance.state = GameManager.GameState.WAVEEND;
    }

    IEnumerator SpawnEnemy(Enemy data)
    {
        SpawnPoint spawn_point = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Vector2 offset = Random.insideUnitCircle * 1.8f;
        
        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject new_enemy = Instantiate(enemy, initial_position, Quaternion.identity);

        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(data.sprite);
        EnemyController en = new_enemy.GetComponent<EnemyController>();
        en.hp = new Hittable(data.hp, Hittable.Team.MONSTERS, new_enemy);
        en.speed = data.speed;
        GameManager.Instance.AddEnemy(new_enemy);
        yield return new WaitForSeconds(0.5f);
    }
}
