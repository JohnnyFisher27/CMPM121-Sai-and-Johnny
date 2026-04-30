using UnityEngine;
using TMPro;
using System.Collections;

public class RewardScreenManager : MonoBehaviour
{
    public GameObject rewardUI;
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI nextText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
