using Enemy;

using UnityEngine;
public class DeserializeJSON
{
    string jsonString = File.ReadAllText(Application.dataPath + "/enemies.json");
    List<Enemy> enemies = DeserializeObject(jsonString);
    Debug.Log(enemies);
}
