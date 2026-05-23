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
    public GameObject relic1;
    public GameObject relic2;
    public GameObject relic3;

    public List<string> relicNames;

    void Start()
    {
        // Relics deserialized
        string jsonString = File.ReadAllText(Application.dataPath + "/Resources/relics.json");
        relics = JsonConvert.DeserializeObject<List<Relic>>(jsonString);

        for (int i = 0; i < relics.Count; i++)
        {
            Debug.Log(relics[i].name);
            Debug.Log(relics[i].sprite);
            Debug.Log(relics[i].trigger.description);
            Debug.Log(relics[i].trigger.type);
            Debug.Log(relics[i].trigger.amount);
            Debug.Log(relics[i].effect.description);
            Debug.Log(relics[i].effect.type);
            Debug.Log(relics[i].effect.amount);
            Debug.Log(relics[i].effect.until);

            relicNames.Add(relics[i].name);
        }

        pickRelic();  
    }

    void pickRelic()
    {
        int num1 = UnityEngine.Random.Range(0, relics.Count);
        int num2 = UnityEngine.Random.Range(0, relics.Count);
        int num3 = UnityEngine.Random.Range(0, relics.Count);

        while (num2 == num1 || num2 == num3)
        {
            num2 = UnityEngine.Random.Range(0, relics.Count);
        }

        while (num3 == num1 || num3 == num2)
        {
            num3 = UnityEngine.Random.Range(0, relics.Count);
        }

        Debug.Log("Randomly picked relic: " + relics[num1].name);
        Debug.Log("Randomly picked relic: " + relics[num2].name);
        Debug.Log("Randomly picked relic: " + relics[num3].name);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
