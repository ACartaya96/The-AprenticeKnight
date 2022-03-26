using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType
{
    Physical,
    Magical,
    Fire,
    Ice,
    Lightning,
    Air,
    Poison,
    Light,
    Dark,
    Blood
}
public class SpellItem : Item
{
    public GameObject spellWarmUpFX;
    public GameObject spellCastFx;
    public string spellAnimation;

   

    [Header("Mana Cost")]
    public float cost;

    [Header(("Spell Type"))]
    public SpellType type;


    [Header("Spell Description")]
    [TextArea]
    public string spellDescription;

    public virtual void AttemptToCastSpell(AnimationHandler animationHandler, PlayerStats playerStats, WeaponSlotManager weaponSlot)
    {
        Debug.Log("You attemp to cast a spell!");
    }

    public virtual void SuccessfullyCastSpell(AnimationHandler animationHandler,PlayerStats playerStats, 
        WeaponSlotManager weaponSlot, PlayerManager playerManager, PlayerTargetDetection playerTarget)
    {
        Debug.Log("You successfully cast a spell!");
    }
}
