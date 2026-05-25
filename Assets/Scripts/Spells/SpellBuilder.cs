using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


public class SpellBuilder 
{

    public Spell Build(SpellCaster owner)
    {
        Debug.Log("SpellCaster speaking !!");
        string jsonString1 = File.ReadAllText(Application.dataPath + "/Resources/spells.json");
        JObject root = JObject.Parse(jsonString1);
        foreach (var entry in root)
        {
            Debug.Log(entry.Key);
        }
       
        return new Spell(owner);
    }

    public SpellBuilder()
    {        
    }

}
