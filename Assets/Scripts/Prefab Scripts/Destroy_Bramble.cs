using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{


    public class Destroy_Bramble : MonoBehaviour, IDamage
    {
        private void Awake()
        {

        }

        public void TakeDamage(float damage, string damageAnimation)
        {
            Destroy(gameObject);
        }

    }
}
