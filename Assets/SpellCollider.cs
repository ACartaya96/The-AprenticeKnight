using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpellCollider : DamageCollider
{
    

    [InlineEditor]
    public SpellItem spell;

    Rigidbody rb;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamage damageable = other.GetComponent<IDamage>();
       
            if (damageable != null)
            {
                damageable.TakeDamage(spell.baseValue);
                projectileLastPos = transform.position;
            }
            Destroy(gameObject);
       
    }
}
