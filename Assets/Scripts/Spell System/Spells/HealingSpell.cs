using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healing Spell", menuName = "Spells/Healing Spell")]
public class HealingSpell : SpellItem
{
    public int healAmount;

    public override void AttemptToCastSpell(AnimationHandler animationHandler, PlayerStats playerStats)
    {
        
        GameObject instatiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, animationHandler.transform);
        animationHandler.PlayTargetAnimation(spellAnimation, true);
        Debug.Log("Attempt to Cast Spell");
    }

    public override void SuccessfullyCastSpell(AnimationHandler animationHandler, PlayerStats playerStats)
    {
        GameObject istantiatedSpellFX = Instantiate(spellCastFx, animationHandler.transform);
        playerStats.HealPlayer(healAmount);
        playerStats.UseMana(cost);
        Debug.Log("Spellcast Successful!");
    }
}
