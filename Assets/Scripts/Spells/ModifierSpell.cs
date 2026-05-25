using UnityEngine;
using System.Collections.Generic;
using System.Text;


public class ModifierSpell
{
    public List<Modifiers> modifiers;
    public SpellCaster myOwner;
    public Spell spell;

    public ModifierSpell(SpellCaster owner)
    {
        modifiers = new List<Modifiers>();
        myOwner = owner;
        spell = new Spell(owner);
    }

    public void addModifier(Modifiers modifier)
    {
        Debug.Log($"Add Modifier:  {modifier.name}");
        modifiers.Add(modifier);
    }
    public Spell getSpell()
    {
        spell = new Spell(myOwner);
        var dict = new Dictionary<string, int> { { "wave", 1 } }; // @todo get current wave johnny halp
        //GameManager.Instance.player.GetComponent<PlayerController>().spellpower += RPNEvaluator.RPNEvaluator.Evaluate("10 wave 5 * +", dict);
        foreach (var modifier in modifiers)
        {
            if (!string.IsNullOrEmpty(modifier.damage_multiplier)) 
            {
                spell.damageMultiplier = RPNEvaluator.RPNEvaluator.Evaluate(modifier.damage_multiplier, dict);
            }
            if (!string.IsNullOrEmpty(modifier.mana_multiplier))
            {
                spell.manaCost *= RPNEvaluator.RPNEvaluator.Evaluate(modifier.mana_multiplier, dict);
            }
            if (!string.IsNullOrEmpty(modifier.delay))
            {
                Debug.Log(modifier.delay);
            }
        }
        return spell;
    }

    public string getDesc()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"{spell.GetName()}");
        Debug.Log($"count: {modifiers.Count}");
        foreach (var modifier in modifiers)
        {
            sb.Append($"{modifier.name}: {modifier.description}");
            Debug.Log($"name: {modifier.name}");
        }
        return sb.ToString();
    }
}
