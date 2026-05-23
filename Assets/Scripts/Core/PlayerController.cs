using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Hittable hp;
    public HealthBar healthui;
    public ManaBar manaui;

    public SpellCaster spellcaster;
    public SpellUI spellui;

    public int speed;

    public Unit unit;

    public int currentSpell = 1;
    public bool[] currentRelics;
    public int spellpower = 10;
    public int nextSpellBuff;
    public bool standingStill;
    public bool playerInvincible;
    public float invincibleTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unit = GetComponent<Unit>();
        GameManager.Instance.player = gameObject;

        currentRelics = new bool[7] {true, true, true, true, true, true, true}; 
        nextSpellBuff = 0;
        invincibleTimer = 0;
        playerInvincible = false;
    }

    void OnEnable()
    {
        EventBus.Instance.OnDamage += playerDamaged;
    } 

    void OnDisable()
    {
        EventBus.Instance.OnDamage -= playerDamaged;
    } 

    public void StartLevel(int health, int mana, int mana_reg)
    {
        spellcaster = new SpellCaster(mana, mana_reg, Hittable.Team.PLAYER);
        StartCoroutine(spellcaster.ManaRegeneration());
        
        hp = new Hittable(health, Hittable.Team.PLAYER, gameObject);
        hp.OnDeath += Die;
        hp.team = Hittable.Team.PLAYER;

        // tell UI elements what to show
        healthui.SetHealth(hp);
        manaui.SetSpellCaster(spellcaster);
        spellui.SetSpell(spellcaster.spell);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInvincible)
        {
            invincibleTimer += Time.deltaTime;
            if (invincibleTimer >= 5f)
            {
                playerInvincible = false;
                invincibleTimer = 0;
            }
        }
    }

    void playerDamaged(Vector3 position, Damage damage, Hittable target)
    {
        if (target == hp)
        {
            if (currentRelics[0] == true)
            {
                spellcaster.mana += 5;
            }
            if (currentRelics[2] == true)
            {
                nextSpellBuff = 100;
            }
            if (currentRelics[6] == true)
            {
                playerInvincible = true;
            }
        }
    }

    void OnAttack(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER 
        || GameManager.Instance.state == GameManager.GameState.GAMEWON || GameManager.Instance.state == GameManager.GameState.WAVEEND) return;
        Vector2 mouseScreen = Mouse.current.position.value;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0;
        StartCoroutine(spellcaster.Cast(transform.position, mouseWorld));
    }

    void OnMove(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER
        || GameManager.Instance.state == GameManager.GameState.GAMEWON || GameManager.Instance.state == GameManager.GameState.WAVEEND) return;
        
        unit.movement = value.Get<Vector2>()*speed;

        if (value.Get<Vector2>() == Vector2.zero)
        {
            standingStill = true;
        } else {
            standingStill = false;
        }
    }

    void OnSwap(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER
        || GameManager.Instance.state == GameManager.GameState.GAMEWON || GameManager.Instance.state == GameManager.GameState.WAVEEND) return;
        
        float inputDirection = value.Get<float>();

        if (inputDirection != 0 && currentSpell + (int)inputDirection > 0 && currentSpell + (int)inputDirection <= 4)
        {
            currentSpell += (int)inputDirection; 
            Debug.Log("Current Spell: " + currentSpell);
        }
    }

    void OnBuff(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER
        || GameManager.Instance.state == GameManager.GameState.GAMEWON || GameManager.Instance.state == GameManager.GameState.WAVEEND) return;
        if (currentRelics[4] == true)
        {
            hp.hp += 1;
        }
    }

    void Die()
    {
        Debug.Log("You Lost");
        GameManager.Instance.state = GameManager.GameState.GAMEOVER;
    }

}

