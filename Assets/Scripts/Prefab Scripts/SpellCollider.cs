using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace TAK
{
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

        public  void OnTriggerEnter(Collider other)
        {
            IDamage damageable = other.GetComponent<IDamage>();

            if (damageable != null)
            {
                damageable.TakeDamage(spell.baseValue, "Damage");
                projectileLastPos = transform.position;
            }
            Destroy(gameObject);

        }
    }
}
