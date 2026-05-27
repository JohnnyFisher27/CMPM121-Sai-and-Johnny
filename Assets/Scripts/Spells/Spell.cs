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
    public float delay;
    public float damageMultiplier;
    public int manaCost;
    public float cooldown;
    public float speed;
    public float angle;
    public string trajectory;

    public Spell(SpellCaster owner)
    {
        this.owner = owner;
        myName = "Bolt";
        delay = 0f;
        damageMultiplier = 1f;
        manaCost = 10;
        cooldown = 0.75f;
        speed = 15f;
        angle = 0f;
        trajectory = "straight";

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
        return (int)(damageMultiplier * (float)(GameManager.Instance.player.GetComponent<PlayerController>().spellpower + GameManager.Instance.player.GetComponent<PlayerController>().nextSpellBuff));
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
        yield return new WaitForSeconds(delay);
        GameManager.Instance.projectileManager.CreateProjectile(0, trajectory, where, target - where, speed, OnHit);
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
