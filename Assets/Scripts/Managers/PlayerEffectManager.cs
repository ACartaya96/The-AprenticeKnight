using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{


    public class PlayerEffectManager : CharacterEffectManager
    {

        public GameObject currentParticleFX; //The particles that will play of the current effect;
        public GameObject instantiatedFXModel;

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

 
        public void HealPlayerFromEffect(int healAmount)
        {
            playerStats.HealPlayer(healAmount);
            GameObject healParticles = Instantiate(currentParticleFX, playerStats.transform);
            Destroy(instantiatedFXModel.gameObject,2);
           
        }

        public void RestoreManaFromEffect(int manaAmount)
        {
            playerStats.RecoverMana(manaAmount);
            GameObject recoverParticles = Instantiate(currentParticleFX, playerStats.transform);
            Destroy(instantiatedFXModel.gameObject,2);
            
        }
        #region Poison
        public override void HandlePoisonBuildUp()
        {
            base.HandlePoisonBuildUp();
            if(isPoisoned == false )
            {
                if (poisonBuildup > 0 && poisonBuildup < 100)
                {
                    poisonStatus.SetCurrentPoisonBuildUp(poisonBuildup);
                }
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
        #endregion
    }
}
