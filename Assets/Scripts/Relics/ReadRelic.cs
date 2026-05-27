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
using TMPro;


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
    public Sprite emptyIcon;

    public Image relicIcon1;
    public Image relicIcon2;
    public Image relicIcon3;

    public TextMeshProUGUI relicText1;
    public TextMeshProUGUI relicText2;
    public TextMeshProUGUI relicText3;

    public GameObject chooseButton1;
    public GameObject chooseButton2;
    public GameObject chooseButton3;

    public int num1;
    public int num2;
    public int num3;


    void Start()
    {
        // Relics deserialized
        string jsonString = File.ReadAllText(Application.dataPath + "/Resources/relics.json");
        relics = JsonConvert.DeserializeObject<List<Relic>>(jsonString);

    }

    public void relicPicks()
    {

        num1 = UnityEngine.Random.Range(0, relics.Count);
        num2 = UnityEngine.Random.Range(0, relics.Count);
        num3 = UnityEngine.Random.Range(0, relics.Count);

        while (GameManager.Instance.player.GetComponent<PlayerController>().currentRelics[num1] == true)
        {
            num1 = UnityEngine.Random.Range(0, relics.Count);
        }

        while (num2 == num1 || num2 == num3 || GameManager.Instance.player.GetComponent<PlayerController>().currentRelics[num2] == true)
        {
            num2 = UnityEngine.Random.Range(0, relics.Count);
        }

        while (num3 == num1 || num3 == num2 || GameManager.Instance.player.GetComponent<PlayerController>().currentRelics[num3] == true)
        {
            num3 = UnityEngine.Random.Range(0, relics.Count);
        }

        Debug.Log("Num1: " + num1 + " Num2: " + num2 + " Num3: " + num3);

        relicText1.text = relics[num1].trigger.description + " " + relics[num1].effect.description;
        relicText2.text = relics[num2].trigger.description + " " + relics[num2].effect.description;
        relicText3.text = relics[num3].trigger.description + " " + relics[num3].effect.description;

        relicIcon1.gameObject.GetComponent<Image>().enabled = true;
        relicIcon2.gameObject.GetComponent<Image>().enabled = true;
        relicIcon3.gameObject.GetComponent<Image>().enabled = true;

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
        if (GameManager.Instance.showRelics == false)
        {
            Debug.Log("False");
            relicIcon1.gameObject.GetComponent<Image>().enabled = false;
            relicIcon2.gameObject.GetComponent<Image>().enabled = false;
            relicIcon3.gameObject.GetComponent<Image>().enabled = false;

            relicText1.text = "";
            relicText2.text = "";
            relicText3.text = "";
        }
        
    }

    public void takeRelic1()
    {
        takeAnyRelic();
        GameManager.Instance.player.GetComponent<PlayerController>().currentRelics[num1] = true;
    }

    public void takeRelic2()
    {
        takeAnyRelic();
        GameManager.Instance.player.GetComponent<PlayerController>().currentRelics[num2] = true;
    }

    public void takeRelic3()
    {
        takeAnyRelic();
        GameManager.Instance.player.GetComponent<PlayerController>().currentRelics[num3] = true;
    }

    void takeAnyRelic()
    {
        chooseButton1.SetActive(false);
        chooseButton2.SetActive(false);
        chooseButton3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
