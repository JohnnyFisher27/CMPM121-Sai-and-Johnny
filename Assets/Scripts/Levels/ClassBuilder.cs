using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ClassBuilder
{
    public List<PlayerClass> playerClass;

    // deserialize classes.json
    public PlayerClass Build()
    {
        string jsonString1 = File.ReadAllText(Application.dataPath + "/Resources/classes.json");
        playerClass = JsonConvert.DeserializeObject<List<PlayerClass>>(jsonString1);
        Debug.Log("ClassBuilder.cs speaking!");
        return playerClass;
    }
}
// return null;

