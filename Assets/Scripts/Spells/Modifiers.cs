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
}
