using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class BlockingColllider : MonoBehaviour
    {
        public BoxCollider blockingCollider;

        public float blockingPhysicalDamageAbsorption;

        private void Awake()
        {
            blockingCollider = GetComponent<BoxCollider>();
        }

        public void SetCollliderDamageAbsorption(WeaponItem weapon)
        {
            if(weapon != null)
            {
                blockingPhysicalDamageAbsorption = weapon.physicalDamageAbsorption;
            }
        }

        public void EnableBlockingCollider()
        {
            blockingCollider.enabled = true;
        }

        public void DisableBlockingCollider()
        {
            blockingCollider.enabled = false;
        }
    }
}
