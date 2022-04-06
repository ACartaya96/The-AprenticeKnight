using System.Collections;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(fileName = "Ranged", menuName = "Spells/Spell Behaviors/Ranged")]
    public class Projectile : SpellBehaviors
    {

        [Header("Pojectile Physics")]
        public float projectileForwardVelocity;
        public float projectileUpwardVelocity;
        public bool isEffecteByGravity;
        public float projectileMass = 1;


        private float minDistance;
        private float maxDistance;
        private bool isRandomOn;
        private float lifeTime;

        Rigidbody rb;
        public Camera cam;

        private void OnEnable()
        {
            Camera cam = Camera.main;
        }



        public virtual void OnActivateEffect(SpellBehaviors spellBase, SpellItem spellItem, WeaponSlotManager weaponSlot, PlayerTargetDetection playerTarget)
        {
            //Get references from the base behavior and
            baseReference = spellBase;
            spell = spellItem;

            GameObject instantiateSpellFX = Instantiate(spellCastFx, weaponSlot.rightHandSlot.transform.position, weaponSlot.rightHandSlot.transform.rotation);
            Destroy(instantiateSpellFX, lifeTime);




            //DamageCollider spellCollider = instantiateSpellFX.GetComponent<DamageCollider>();
            // spellCollider.EnableDamageCollider();
            //spellLastPos = spellCollider.projectileLastPos;
            //Set Physics

            rb = instantiateSpellFX.GetComponent<Rigidbody>();




            if (playerTarget.currentLockedOnTarget != null)
            {
                Vector3 dir = playerTarget.currentLockedOnTarget.position - instantiateSpellFX.transform.position;

                dir.Normalize();

                Quaternion tr = Quaternion.LookRotation(dir);
                Quaternion targetRotation = Quaternion.Slerp(instantiateSpellFX.transform.rotation, tr, projectileForwardVelocity * Time.deltaTime);
                instantiateSpellFX.transform.rotation = targetRotation;
                instantiateSpellFX.transform.position = Vector3.MoveTowards(instantiateSpellFX.transform.position, playerTarget.currentLockedOnTarget.transform.position, projectileForwardVelocity * Time.deltaTime / 5f);
                //instantiateSpellFX.transform.LookAt(playerTarget.currentLockedOnTarget);

            }
            else
            {
                instantiateSpellFX.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, playerTarget.targetTransform.transform.eulerAngles.y, 0);
            }

            rb.AddForce(instantiateSpellFX.transform.forward * projectileForwardVelocity);
            rb.AddForce(instantiateSpellFX.transform.up * projectileUpwardVelocity);


            // Debug.Log(instantiateSpellFX.transform.position.ToString());
            rb.useGravity = isEffecteByGravity;
            rb.mass = projectileMass;
            //instantiateSpellFX.transform.parent = null;

        }

        private void OnTriggerEnter(Collider other)
        {
            IDamage damageable = other.GetComponent<IDamage>();

            if (damageable != null)
            {
                damageable.TakeDamage(spell.baseValue, "Damage");
            }
            Destroy(this);
        }

        private void OnTriggerStay(Collider other)
        {

        }

        private void OnTriggerExit(Collider other)
        {

        }
    }   
}
