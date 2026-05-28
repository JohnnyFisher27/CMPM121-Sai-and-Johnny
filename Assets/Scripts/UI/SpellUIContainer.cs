using UnityEngine;

public class SpellUIContainer : MonoBehaviour
{
    public GameObject[] spellUIs;
    public PlayerController player;
    public GameObject chooseButton;
    public int numSpells = 1;

    public GameObject dropButton2;
    public GameObject dropButton3;
    public GameObject dropButton4;
    public GameObject[] dropButtons;

    public void takeSpell()
    {
        if (numSpells >= 4) {
            Debug.Log("Max Spells Reached");
            return;
        }
        chooseButton.SetActive(false);
        numSpells++;
        spellUIs[numSpells - 1].SetActive(true);

        GameManager.Instance.player.GetComponent<PlayerController>().allSpells.Add(GameManager.Instance.player.GetComponent<PlayerController>().currentSpell);

        GameManager.Instance.player.GetComponent<PlayerController>().spellUIComponents[numSpells - 1].SetSpell(GameManager.Instance.player.GetComponent<PlayerController>().currentSpell);

        foreach (GameObject i in dropButtons)
        {
            i.SetActive(false);
        }
        setDropButton();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spellUIs[0].SetActive(true);

        dropButtons = new GameObject[3] {dropButton2, dropButton3, dropButton4};

    }

    public void dropSpell2()
    {
        spellUIs[1].SetActive(false);

        GameManager.Instance.player.GetComponent<PlayerController>().allSpells.RemoveAt(numSpells - 1);
        numSpells--;
    }

    public void dropSpell3()
    {
        spellUIs[2].SetActive(false);

        GameManager.Instance.player.GetComponent<PlayerController>().allSpells.RemoveAt(numSpells - 1);
        numSpells--;
        setDropButton();
    }

    public void dropSpell4()
    {
        spellUIs[3].SetActive(false);

        GameManager.Instance.player.GetComponent<PlayerController>().allSpells.RemoveAt(numSpells - 1);
        numSpells--;
        setDropButton();
    }

    public void setDropButton()
    {
        if (numSpells >= 2)
        {
            dropButtons[numSpells - 2].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
