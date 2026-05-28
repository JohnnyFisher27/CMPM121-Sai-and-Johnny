using UnityEngine;

public class SpellUIContainer : MonoBehaviour
{
    public GameObject[] spellUIs;
    public PlayerController player;
    public GameObject chooseButton;
    static int numSpells = 1;
    public void takeSpell()
    {
        if (numSpells >= 4) {
            Debug.Log("Max Spells Reached");
            return;
        }
        chooseButton.SetActive(false);     
        numSpells++;
        Debug.Log(numSpells);
        spellUIs[numSpells - 1].SetActive(true);

        GameManager.Instance.player.GetComponent<PlayerController>().allSpells.Add(GameManager.Instance.player.GetComponent<PlayerController>().currentSpell);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // we only have one spell (right now)
        spellUIs[0].SetActive(true);

        /*for(int i = 1; i< spellUIs.Length; ++i)
        {
            spellUIs[i].SetActive(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
