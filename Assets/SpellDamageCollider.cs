using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpellDamageCollider : DamageCollider
{
    [InlineEditor]
    public SpellItem spell;
    // Start is called before the first frame update
    void Start()
    {
      
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
