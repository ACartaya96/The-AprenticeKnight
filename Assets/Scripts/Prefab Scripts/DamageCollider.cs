using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;
    [SerializeField] WeaponItem weaponItem;
    [SerializeField] ProjectileSpell projectileSpell;
    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
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
        
        if(other != null)
        {
            IDamage damageable = other.GetComponent<IDamage>();
            if (weaponItem != null)
            {
                if (damageable != null)
                    damageable.TakeDamage(weaponItem.meleeDamage);
            }
            else if (projectileSpell != null)
            {
                if (damageable != null)
                {
                    damageable.TakeDamage(projectileSpell.baseDamage);
                }
                Destroy(gameObject);
            }
        }
    }
}