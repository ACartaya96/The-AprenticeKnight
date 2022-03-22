using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellItem : Item
{
    public GameObject spellWarmUpFX;
    public GameObject spellCastFx;
    public string spellAnimation;

    [Header("Mana Cost")]
    public float cost;

    [Header(("Spell Type"))]
    public SpellType type;
    public enum SpellType
    {
       Melee,
       Sorcery
    }

    [Header("Spell Description")]
    [TextArea]
    public string spellDescription;

    public virtual void AttemptToCastSpell(AnimationHandler animationHandler, PlayerStats playerStats)
    {
        Debug.Log("You attemp to cast a spell!");
    }

    public virtual void SuccessfullyCastSpell(AnimationHandler animationHandler,PlayerStats playerStats)
    {
        Debug.Log("You successfully cast a spell!");
    }
}
