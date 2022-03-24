using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healing Spell", menuName = "Spells/Healing Spell")]
public class HealingSpell : SpellItem
{
    public int healAmount;

    public override void AttemptToCastSpell(AnimationHandler animationHandler, PlayerStats playerStats, WeaponSlotManager weaponSlot)
    {
        
        GameObject instatiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlot.rightHandSlot.transform);
        animationHandler.PlayTargetAnimation(spellAnimation, true);
        Debug.Log("Attempt to Cast Spell");
    }

    public override void SuccessfullyCastSpell(AnimationHandler animationHandler, PlayerStats playerStats, WeaponSlotManager weaponSlot, PlayerManager playerManager)
    {
        GameObject istantiatedSpellFX = Instantiate(spellCastFx, weaponSlot.rightHandSlot.transform.root);
        playerStats.HealPlayer(healAmount);
        playerStats.UseMana(cost);
        Debug.Log("Spellcast Successful!");
    }
}
