using UnityEngine;

public class SetSpells : MonoBehaviour
{
    public GameObject spell1;
    public GameObject spell2;
    public GameObject spell3;
    public GameObject spell4;
    public GameObject chooseButton;
    static int numSpells = 1;
    public void takeSpell()
    {
        chooseButton.SetActive(false);  
        numSpells++;
        if (numSpells == 2)
            spell2.SetActive(true);
        else if (numSpells == 3)
            spell3.SetActive(true);
        else if (numSpells == 4)
            spell4.SetActive(true);
        else
            Debug.Log("Max Spells Reached");
    }
}
