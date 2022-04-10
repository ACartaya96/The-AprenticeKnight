using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(menuName = "A.I./Item/Spell/Projectile")]
    public class EnemyProjectileSpell : EnemySpellItem
    {
        [Header("Pojectile Damage")]
        public float baseDamage;
        public float buildUp;

        [Header("Pojectile Physics")]
        public float projectileForwardVelocity;
        public float projectileUpwardVelocity;
        public bool isEffecteByGravity;
        public float projectileMass = 1;




        Rigidbody rb;

        Camera cam;

        private void OnEnable()
        {

           
            baseValue = baseDamage;
            //effectBuildUp = buildUp;

        }

        public override void AttemptToCastSpell(EnemyAnimationHandler animationHandler, EnemyStats enemyStats, EnemyAudioManager audioManager, EnemyManager enemyManager)
        {
            GameObject istantiateWarmUpSpellFX = Instantiate(spellWarmUpFX, enemyManager.castPoint.transform);
            //istantiateWarmUpSpellFX.gameObject.transform.localScale = new Vector3(100, 100, 100);
            animationHandler.PlayTargetAnimation(spellAnimation, true);
            audioManager.PlayTargetSoundEffect(startUpSFX);
        }

        public override void SuccessfullyCastSpell(EnemyAnimationHandler animationHandler, EnemyStats enemyStats, EnemyAudioManager audioManager, EnemyManager enemyManager, CharacterEffectManager effectManager)
        {
            GameObject instantiateSpellFX = Instantiate(spellCastFx, enemyManager.castPoint.transform.position, enemyManager.castPoint.transform.rotation);
            Destroy(instantiateSpellFX, 8f);
            Debug.Log(instantiateSpellFX.transform.position.ToString());
            //spelldamageCollider


            //DamageCollider spellCollider = instantiateSpellFX.GetComponent<DamageCollider>();
            // spellCollider.EnableDamageCollider();
            //spellLastPos = spellCollider.projectileLastPos;
            //Set Physics
            rb = instantiateSpellFX.GetComponent<Rigidbody>();




            if (enemyManager.currentTarget != null)
            {
                /*Vector3 dir = playerTarget.currentLockedOnTarget.position - instantiateSpellFX.transform.position;

                dir.Normalize();

                Quaternion tr = Quaternion.LookRotation(dir);
                Quaternion targetRotation = Quaternion.Slerp(instantiateSpellFX.transform.rotation, tr, projectileForwardVelocity * Time.deltaTime);
                instantiateSpellFX.transform.rotation = targetRotation;
                instantiateSpellFX.transform.position = Vector3.MoveTowards(instantiateSpellFX.transform.position, playerTarget.currentLockedOnTarget.transform.position, projectileForwardVelocity * Time.deltaTime/5f);*/
                instantiateSpellFX.transform.LookAt(enemyManager.currentTarget.transform.position);

            }
            else
            {
                /*Vector3 aimSpot = cam.transform.position;
                 aimSpot += cam.transform.forward * 50;
                 instantiateSpellFX.transform.LookAt(aimSpot);*/

                instantiateSpellFX.transform.rotation = Quaternion.Euler(enemyManager.transform.eulerAngles.x, enemyManager.transform.eulerAngles.y, 0);

            }

            rb.AddForce(instantiateSpellFX.transform.forward * projectileForwardVelocity);
            rb.AddForce(instantiateSpellFX.transform.up * projectileUpwardVelocity);


            Debug.Log(instantiateSpellFX.transform.position.ToString());
            rb.useGravity = isEffecteByGravity;
            rb.mass = projectileMass;
            instantiateSpellFX.transform.parent = null;

            //Damage and Cost
            //playerStats.UseMana(cost);
        }

      

        public override void ApplySpellEffects()
        {
            Debug.Log("Applying Spell Effects");
        }
    }   
}
