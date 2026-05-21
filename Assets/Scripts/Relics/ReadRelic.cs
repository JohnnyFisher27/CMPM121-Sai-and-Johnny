using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RPNEvaluator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReadRelic : MonoBehaviour
{
    public List<Relic> relics;

    void Start()
    {
        // Relics deserialized
        string jsonString = File.ReadAllText(Application.dataPath + "/Resources/relics.json");
        relics = JsonConvert.DeserializeObject<List<Relic>>(jsonString);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
