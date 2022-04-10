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

        [Header("Immunities")]
        public bool immuneToPoison;

        public bool check = false;
        public bool resetDps = false;
        public bool stunned = false;


        protected virtual void Awake()
        {
            characterStatsManager = GetComponentInParent<CharacterStatsManager>();
        }

        #region Damage By Effect
        public IEnumerator TakeDamageByFlagType(Spell spell, Transform target)
        {
            if (spell.spellEffect == Spell.SpellEffect.Slow)
            {

                Debug.Log("Slowed");

            }

            else if (spell.spellEffect == Spell.SpellEffect.DamagePerSecond)
            {
                if (resetDps && check)
                {
                    check = false;
                    resetDps = false;
                    StopAllCoroutines();
                }

                if (!check)
                    StartCoroutine(DOT(spell.dotDamage, spell.dotTick, spell.dotDuration, spell.dotEffect, target));

            }

            else
            {
                Debug.Log("don't have spell effect.");
                yield break;
            }


        }

        public IEnumerator DOT(int damage, int tick, int time, GameObject dotEffect, Transform target)
        {

            int count = 0;

            check = true;


            while (count < tick)
            {
                yield return new WaitForSeconds(time);
                target.gameObject.GetComponent<IDamage>().TakeDamage(damage, null);
                Instantiate(dotEffect, target.position, Quaternion.identity);
                count++;

            }

            check = false;
        }
        #endregion


        #region Build Up Effects
        public virtual void HandleAllBuildUpEffects()
        {
            if (characterStatsManager.isDead)
            {
                return;
            }
            if (!immuneToPoison)
            {
                HandlePoisonBuildUp();
                HandlePoisonedEffect();
            }
        }

        public virtual void HandlePoisonBuildUp()
        {
            if (isPoisoned == true)
            {
                return;
            }
                
            if (characterStatsManager.isDead)
            {
                return;
            }
              

            if (poisonBuildup > 0 && poisonBuildup < 100)
            {
                poisonBuildup = poisonBuildup - 1 * Time.deltaTime/8;
            }
            else if (poisonBuildup >= 100)
            {
                isPoisoned = true;
                poisonBuildup = 0;
            }
        }

     
        public virtual void HandlePoisonedEffect()
        {
            if (isPoisoned == true)
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
                    poisonBuildup = defaultPoisonAmount;
                }
            }
        }
        #endregion
    }

}