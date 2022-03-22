using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    Collider damageCollider;
    [SerializeField] WeaponItem weaponItem;
    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        weaponItem = GetComponent<WeaponItem>();
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
        IDamage damageable = other.GetComponent<IDamage>();
        if(damageable != null)
        {
            damageable.TakeDamage(weaponItem.meleeDamage);
        }
    }
}
