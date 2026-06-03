using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;


[Serializable]
public class PlayerClass : MonoBehaviour
{
    public int sprite;
    public string health;
    public string mana;
    public string mana_regeneration;
    public string spellpower;
    public string speed;
    public PlayerClass Clone()
    {
        return new PlayerClass
        {
            sprite = this.sprite,
            health = this.health,
            mana = this.mana,
            mana_regeneration = this.mana_regeneration,
            spellpower = this.spellpower,
            speed = this.speed,
        };
    }
}


