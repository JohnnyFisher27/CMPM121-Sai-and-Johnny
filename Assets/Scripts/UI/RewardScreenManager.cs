using UnityEngine;
using TMPro;
using System.Collections;

public class RewardScreenManager : MonoBehaviour
{
    public GameObject rewardUI;
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI nextText;

    // This is for taking the spell
    public GameObject chooseButton;

    // These are for taking relics
    public GameObject chooseButton1;
    public GameObject chooseButton2;
    public GameObject chooseButton3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chooseButton1.SetActive(false);
        chooseButton2.SetActive(false);
        chooseButton3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.INWAVE) {
            chooseButton.SetActive(true);
            if (GameManager.Instance.showRelics)
            {
                chooseButton1.SetActive(true);
                chooseButton2.SetActive(true);
                chooseButton3.SetActive(true);
            }
            
        }

        if (GameManager.Instance.state == GameManager.GameState.WAVEEND)
        {
            displayText.text = $"Wave took {Mathf.Round(GameManager.Instance.wave_time)} seconds";
            nextText.text = "Next Wave";
            rewardUI.SetActive(true);
        }
        else if (GameManager.Instance.state == GameManager.GameState.GAMEWON)
        {
            displayText.text = "You win!";
            nextText.text = "Reset";
            rewardUI.SetActive(true);
        }
        else if (GameManager.Instance.state == GameManager.GameState.GAMEOVER)
        {
            displayText.text = "You Died";
            nextText.text = "Reset";
            rewardUI.SetActive(true);
        }
        else
        {
            rewardUI.SetActive(false);
        }
    }
}
