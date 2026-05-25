using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


public class SpellBuilder 
{

    public Spell Build(SpellCaster owner)
    {
        string jsonString1 = File.ReadAllText(Application.dataPath + "/Resources/spells.json");
        string jsonString2 = File.ReadAllText(Application.dataPath + "/Resources/modifiers.json");
        JObject root = JObject.Parse(jsonString1);
        JObject root2 = JObject.Parse(jsonString2);
        foreach (var entry in root)
        {
            Debug.Log(entry.Key);
        }
        foreach (var entry in root2)
        {
            Debug.Log(entry.Key);
        }
        Debug.Log("SpellCaster speaking !!");

        return new Spell(owner);
    }

    public SpellBuilder()
    {        
    }

}
