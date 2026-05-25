using UnityEngine;
using System.Collections.Generic;
using System.Text;


public class ModifierSpell
{
    public List<Modifiers> modifiers;
    public Spell spell;

    public ModifierSpell(SpellCaster owner)
    {
        modifiers = new List<Modifiers>();
        spell = new Spell(owner);
    }

    public void addModifier(Modifiers modifier)
    {
        Debug.Log($"Add Modifier:  {modifier.name}");
        modifiers.Add(modifier);
    }
    public Spell getSpell()
    {
        return spell;
    }

    public string getDesc()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"{spell.GetName()} \n");
        Debug.Log($"count: {modifiers.Count}");
        foreach (var modifier in modifiers)
        {
            sb.Append($"{modifier.name}: {modifier.description} \n");
            Debug.Log($"name: {modifier.name}");
        }
        return sb.ToString();
    }
}
