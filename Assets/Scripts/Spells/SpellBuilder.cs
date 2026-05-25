using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


public class SpellBuilder 
{

    public List<Modifiers> modifiers;

    public Spell Build(SpellCaster owner)
    {
        string jsonString1 = File.ReadAllText(Application.dataPath + "/Resources/modifiers.json");
        modifiers = JsonConvert.DeserializeObject<List<Modifiers>>(jsonString1);
        ModifierSpell modifierSpell = new ModifierSpell(owner);
        int numModifiers = Random.Range(1, 4);
        for (int ii = 0; ii < numModifiers; ii++)
        {
            int index = Random.Range(0, modifiers.Count - 1);
            Debug.Log($"modifier: {ii}, name: {modifiers[index].name}");
            modifierSpell.addModifier(modifiers[index]);
        }
        //return new Spell(owner);
        Debug.Log(modifierSpell.getDesc());
        return modifierSpell.getSpell();
    }

    public SpellBuilder()
    {        
    }

}
