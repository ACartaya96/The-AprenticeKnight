using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{


    public class PlayerEffectManager : CharacterEffectManager
    {
        PlayerStats playerStats;
        public PoisonStatusBar poisonStatus;
        WeaponSlotManager weaponSlotManager;
        protected override void Awake()
        {
            base.Awake();
            playerStats = GetComponentInParent<PlayerStats>();
         
            weaponSlotManager = GetComponent<WeaponSlotManager>();
            poisonStatus.setMaxPoisonBuildUp(defaultPoisonAmount);
            poisonStatus.SetCurrentPoisonBuildUp(0);
        }
        public override void HandlePoisonBuildUp()
        {
            base.HandlePoisonBuildUp();
            if(isPoisoned == false)
            {
                poisonStatus.SetCurrentPoisonBuildUp(poisonBuildup);
            }
           
        }

       public override void HandlePoisonedEffect()
        {
            base.HandlePoisonedEffect();
            if(isPoisoned == true)
            {
                poisonStatus.SetCurrentPoisonBuildUp(poisonAmount);
            }
          
        }
    }
}
