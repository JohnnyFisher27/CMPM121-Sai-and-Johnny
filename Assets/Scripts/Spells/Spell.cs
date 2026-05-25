using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Spell 
{
    public float last_cast;
    public SpellCaster owner;
    public Hittable.Team team;
    public string myName;
    public int damageMultiplier;
    public int manaCost;
    public float cooldown;
    public float speed;

    public Spell(SpellCaster owner)
    {
        this.owner = owner;
        myName = "Bolt";
        damageMultiplier = 1;
        manaCost = 10;
        cooldown = 0.75f;
        speed = 15f;

    }

    public string GetName()
    {
        return myName;
    }

    public int GetManaCost()
    {
        return manaCost;
    }

    public int GetDamage()
    {
        
        return (GameManager.Instance.player.GetComponent<PlayerController>().spellpower 
            + GameManager.Instance.player.GetComponent<PlayerController>().nextSpellBuff) * damageMultiplier;
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    public virtual int GetIcon()
    {
        return 0;
    }

    public bool IsReady()
    {
        return (last_cast + GetCooldown() < Time.time);
    }

    public virtual IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(0, "straight", where, target - where, speed, OnHit);

        yield return new WaitForEndOfFrame();
    }

    void OnHit(Hittable other, Vector3 impact)
    {
        if (other.team != team)
        {
            other.Damage(new Damage(GetDamage(), Damage.Type.ARCANE));
            GameManager.Instance.player.GetComponent<PlayerController>().nextSpellBuff = 0;
        }

    }

}
