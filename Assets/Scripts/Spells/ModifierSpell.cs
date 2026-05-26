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
        var dict = new Dictionary<string, int> { { "wave", 1 } }; // @todo get current wave JOHNNY HELP

        // GameManager.Instance.player.GetComponent<PlayerController>().spellpower += RPNEvaluator.RPNEvaluator.Evaluate("10 wave 5 * +", dict);

        foreach (var modifier in modifiers)
        {
            // delay meow
            if (!string.IsNullOrEmpty(modifier.delay))
            {
                spell.delay = RPNEvaluator.RPNEvaluator.Evaluatef(modifier.delay, dict);
                Debug.Log(modifier.delay);
            }

            // angle meow
            if (!string.IsNullOrEmpty(modifier.angle))
            {
                // :3 @todo cuz idk the vision here
            }

            // damage meow
            if (!string.IsNullOrEmpty(modifier.damage_multiplier))
            {
                spell.damageMultiplier = RPNEvaluator.RPNEvaluator.Evaluatef(modifier.damage_multiplier, dict);
            }

            // mana multiplier meow
            if (!string.IsNullOrEmpty(modifier.mana_multiplier))
            {
                spell.manaCost = (int)((float)spell.manaCost * RPNEvaluator.RPNEvaluator.Evaluatef(modifier.mana_multiplier, dict));
            }

            // mana adder meow
            if (!string.IsNullOrEmpty(modifier.mana_adder))
            {
                spell.manaCost += RPNEvaluator.RPNEvaluator.Evaluate(modifier.mana_adder, dict);
            }

            // speed meow
            if (!string.IsNullOrEmpty(modifier.speed_multiplier))
            {
                spell.speed *= RPNEvaluator.RPNEvaluator.Evaluatef(modifier.speed_multiplier, dict);
            }

            // cooldown meow
            if (!string.IsNullOrEmpty(modifier.cooldown_multiplier))
            {
                spell.cooldown *= RPNEvaluator.RPNEvaluator.Evaluatef(modifier.cooldown_multiplier, dict);
            }

            // projectile trajectory meow
            if (!string.IsNullOrEmpty(modifier.projectile_trajectory))
            {
                spell.trajectory = modifier.projectile_trajectory;
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
