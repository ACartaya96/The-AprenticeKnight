using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class CharacterEffectManager : MonoBehaviour
    {
        //[Header("Damage FX")]
        //public GameObject bloodSplatter;
        CharacterStatsManager characterStatsManager;
        [Header("Poison")]
        public bool isPoisoned;
        public float poisonBuildup = 0;//build up over time
        public float poisonAmount = 100;//The amount of poison the player has to process before poison
        public float defaultPoisonAmount;
        public float poisonDamage = 1;


        protected virtual void Awake()
        {
            characterStatsManager = GetComponentInParent<CharacterStatsManager>();
        }

        public virtual void HandleAllBuildUpEffects()
        {
            if (characterStatsManager.isDead)
            {
                return;
            }
            HandlePoisonBuildUp();
            HandlePoisonedEffect();
        }

        protected virtual void HandlePoisonBuildUp()
        {
            if (isPoisoned)
                return;

            if (characterStatsManager.isDead)
                return;

            if (poisonBuildup > 0 && poisonBuildup < 100)
            {
                poisonBuildup = poisonBuildup - 1 * Time.deltaTime;
            }
            else if (poisonBuildup >= 100)
            {
                isPoisoned = true;
                poisonBuildup = 0;
            }
        }

     
        protected virtual void HandlePoisonedEffect()
        {
            if (isPoisoned)
            {
                if (poisonAmount > 0)
                {
                    //Damage Player
                    IDamage damageable = GetComponentInParent<IDamage>();
                    damageable.TakeDamage(poisonDamage, null);
                    poisonAmount = poisonAmount - 1 * Time.deltaTime;
                }
                else
                {
                    isPoisoned = false;
                    poisonAmount = defaultPoisonAmount;
                }
            }
        }
    }

}