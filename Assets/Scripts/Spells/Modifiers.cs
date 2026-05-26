using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class Modifiers : MonoBehaviour
{
    public string name;
    public string description;
    public string delay;
    public string angle;
    public string damage_multiplier;
    public string mana_multiplier;
    public string mana_adder;
    public string speed_multiplier;
    public string cooldown_multiplier;
    public string projectile_trajectory;
    public Modifiers Clone()
    {
        return new Modifiers
        {
            name = this.name,
            description = this.description,
            delay = this.delay,
            angle = this.angle,
            damage_multiplier = this.damage_multiplier,
            mana_multiplier = this.mana_multiplier,
            mana_adder = this.mana_adder,
            speed_multiplier = this.speed_multiplier,
            cooldown_multiplier = this.cooldown_multiplier,
            projectile_trajectory = this.projectile_trajectory
        };
    }
}
