using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
       
        [SerializeField] WeaponItem weaponItem;


        [HideInInspector]
        public Vector3 projectileLastPos;

        public bool enableOnStartUp;
        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = enableOnStartUp;

        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }
        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamage damageable = other.GetComponent<IDamage>();
            CharacterManager character = other.GetComponentInParent<CharacterManager>();
          
            BlockingColllider block = other.GetComponentInChildren<BlockingColllider>();
            if (other != null)
            {   
                if (weaponItem != null)
                {
                    if(character.isBlocking )
                    {
                        if (block != null)
                        {
                            Debug.Log(character.name.ToString() + " Blocked");
                            float physicalDamageAfterBlock = weaponItem.baseDamage - (weaponItem.baseDamage * block.blockingPhysicalDamageAbsorption) / 100;
                            if (damageable != null)
                                damageable.TakeDamage(physicalDamageAfterBlock, "Blocked");
                        }
                    }
                    else
                    {
                        Debug.Log(character.name.ToString() + " Got Hit");
                        if (damageable != null)
                            damageable.TakeDamage(weaponItem.baseDamage, "Damage");
                    }
                    
                }

            }

        }
    }
}