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

    public Sprite greenGemIcon;
    public Sprite jadeElephantIcon;
    public Sprite goldenMaskIcon;
    public Sprite cursedScrollIcon;
    public Sprite massiveBongIcon;
    public Sprite mommasMittensIcon;
    public Sprite superArmorIcon;

    public Image relicIcon1;
    public Image relicIcon2;
    public Image relicIcon3;

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

        }

    }

    public void pickRelic()
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

        switch (relics[num1].sprite) {
            case 0:
                relicIcon1.sprite = greenGemIcon;
                break;
            case 1:
                relicIcon1.sprite = jadeElephantIcon;
                break;
            case 2:
                relicIcon1.sprite = goldenMaskIcon;
                break;
            case 3:
                relicIcon1.sprite = cursedScrollIcon;
                break;
            case 4:
                relicIcon1.sprite = massiveBongIcon;
                break;
            case 5:
                relicIcon1.sprite = mommasMittensIcon;
                break;
            case 6:
                relicIcon1.sprite = superArmorIcon;
                break;
            default:
                Debug.Log("Failed to get num1 sprite");
                break;
        }
        switch (relics[num2].sprite) {
            case 0:
                relicIcon2.sprite = greenGemIcon;
                break;
            case 1:
                relicIcon2.sprite = jadeElephantIcon;
                break;
            case 2:
                relicIcon2.sprite = goldenMaskIcon;
                break;
            case 3:
                relicIcon2.sprite = cursedScrollIcon;
                break;
            case 4:
                relicIcon2.sprite = massiveBongIcon;
                break;
            case 5:
                relicIcon2.sprite = mommasMittensIcon;
                break;
            case 6:
                relicIcon2.sprite = superArmorIcon;
                break;
            default:
                Debug.Log("Failed to get num2 sprite");
                break;
        }
        switch (relics[num3].sprite) {
            case 0:
                relicIcon3.sprite = greenGemIcon;
                break;
            case 1:
                relicIcon3.sprite = jadeElephantIcon;
                break;
            case 2:
                relicIcon3.sprite = goldenMaskIcon;
                break;
            case 3:
                relicIcon3.sprite = cursedScrollIcon;
                break;
            case 4:
                relicIcon3.sprite = massiveBongIcon;
                break;
            case 5:
                relicIcon3.sprite = mommasMittensIcon;
                break;
            case 6:
                relicIcon3.sprite = superArmorIcon;
                break;
            default:
                Debug.Log("Failed to get num3 sprite");
                break;
        }   
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
