using UnityEngine;
using System;
using System.Collections.Generic;


[Serializable]
public class Relic : MonoBehaviour
{
    public string name;
    public int sprite;
    public TriggerData trigger;
    public EffectData effect;
}

[Serializable]
public class TriggerData
{
    public string description;
    public string type;
    public string amount;
}

[Serializable]
public class EffectData
{
    public string description;
    public string type;
    public string amount; 
    public string until; 
}

