using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(fileName = "Healing Spell", menuName = "Spells/Healing Spell")]
    public class HealingSpell : SpellItem
    {
        public int healAmount;

        public override void AttemptToCastSpell(AnimationHandler animationHandler, PlayerStats playerStats, WeaponSlotManager weaponSlot,PlayerAudioManager audioManager)
        {

            GameObject instatiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlot.rightHandSlot.transform);
            animationHandler.PlayTargetAnimation(spellAnimation, true, false);
            Debug.Log("Attempt to Cast Spell");
            audioManager.PlayTargetSoundEffect(startUpSFX);
        }

        public override void SuccessfullyCastSpell(AnimationHandler animationHandler, PlayerStats playerStats, WeaponSlotManager weaponSlot, PlayerManager playerManager, PlayerTargetDetection playerTarget,PlayerAudioManager audioManager)
        {
            //GameObject istantiatedSpellFX = Instantiate(spellCastFx, weaponSlot.rightHandSlot.transform.root);
            playerStats.HealPlayer(healAmount);
            playerStats.UseMana(cost);
            Debug.Log("Spellcast Successful!");
        }
    }
}
