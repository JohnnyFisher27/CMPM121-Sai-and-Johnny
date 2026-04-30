using UnityEngine;
using System;
using System.Collections.Generic;


[Serializable]
public class Levels
{
    public string name;
    public int waves;
    public List<SpawnList> spawns;
}

[Serializable]
public class SpawnList
{
    public string enemy;
    public string count;
    public string speed;
    public string hp;
    public string delay;
    public List<int> sequence;
    public string damage;
    public string location;
}