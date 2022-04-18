using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(menuName = "Item/Consumables/Elixers")]
    public class Elixers : ConsumableItem
    {
        public enum ElixerType
        {
            Life,
            Mana
        }

        [Header("Elixer Type")]
        public ElixerType elixer;

        [Header("Recovery Amount")]
        public int RecoveryAmount;

        [Header("Recover FX")]
        public GameObject recoverFX;

        private void Awake()
        {
            currentItemAmount = maxAmount;
        }
        public override void AttemptToConsumeItem(AnimationHandler animationHandler, WeaponSlotManager weaponSlotManager, PlayerEffectManager playerEffectManager)
        {
            base.AttemptToConsumeItem(animationHandler, weaponSlotManager, playerEffectManager);
            GameObject flask = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
            if (elixer == ElixerType.Life)
            {
                playerEffectManager.currentParticleFX = recoverFX;
                playerEffectManager.HealPlayerFromEffect(RecoveryAmount);
            }
            else if(elixer == ElixerType.Mana)
            {
                playerEffectManager.currentParticleFX = recoverFX;
                playerEffectManager.RestoreManaFromEffect(RecoveryAmount);
            }
            playerEffectManager.instantiatedFXModel = flask;
          
            weaponSlotManager.rightHandSlot.UnloadWeapon();
           
            //Play FX when succesfully drank
        }

    }

  
}
