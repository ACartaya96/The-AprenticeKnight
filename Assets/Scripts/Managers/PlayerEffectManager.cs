using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{


    public class PlayerEffectManager : CharacterEffectManager
    {
        PlayerStats playerStats;
        WeaponSlotManager weaponSlotManager;
        protected override void Awake()
        {
            base.Awake();
            playerStats = GetComponentInParent<PlayerStats>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
        }
        protected override void HandlePoisonBuildUp()
        {
            base.HandlePoisonBuildUp();
        }

        protected override void HandlePoisonedEffect()
        {
            base.HandlePoisonedEffect();
        }
    }
}
