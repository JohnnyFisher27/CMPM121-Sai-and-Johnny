using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


public class SpellBuilder 
{

    public List<Modifiers> modifiers;
    public string description;

    public Spell Build(SpellCaster owner, bool baseSpell)
    {
        string jsonString1 = File.ReadAllText(Application.dataPath + "/Resources/modifiers.json");
        modifiers = JsonConvert.DeserializeObject<List<Modifiers>>(jsonString1);

        ModifierSpell modifierSpell = new ModifierSpell(owner);
        int numModifiers = 0;
        if (!baseSpell)
        {
            numModifiers = Random.Range(2, 4);
        }

        for (int ii = 0; ii < numModifiers; ii++)
        {
            int index = Random.Range(0, modifiers.Count);
            //Debug.Log($"modifier: {ii}, name: {modifiers[index].name}");
            Modifiers modifier = modifiers[index].Clone();
            modifierSpell.addModifier(modifier);
        }
        //return new Spell(owner);
        
        description = modifierSpell.getDesc();
        return modifierSpell.getSpell();
    }

    public SpellBuilder()
    {   
        
    }

}
