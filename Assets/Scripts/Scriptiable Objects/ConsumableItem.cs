using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{ 
    public class ConsumableItem : Item
    {
        [Header("Item Quantity")]
        public int maxAmount;
        public int currentItemAmount;

        [Header("Item Model")]
        public GameObject itemModel;

        [Header("Animations")]
        public string consumableAnimation;
        public bool isInteracting;

        public virtual void AttemptToConsumeItem(AnimationHandler animationHandler, WeaponSlotManager weaponSlotManager, PlayerEffectManager playerEffectManager)
        {
            if(currentItemAmount >= 0)
            {
                animationHandler.PlayTargetAnimation(consumableAnimation, isInteracting, true);
            }
            else
            {
                //animationHandler.PlayTargetAnimation("Shrug", true);
            }
        }
    }
}
