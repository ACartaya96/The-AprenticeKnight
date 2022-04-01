using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{

    public class EnemyAttack : MonoBehaviour
    {
        public DamageCollider damageCollider;
        public void OpenRightDamageCollider()
        {
            damageCollider.EnableDamageCollider();
        }
        public void OpenLeftDamageCollider()
        {
            //leftDamageCollider.EnableDamageCollider();
        }
        public void CloseRightDamageCollider()
        {
            damageCollider.DisableDamageCollider();
        }
        public void CloseLeftDamageCollider()
        {
            //leftDamageCollider.DisableDamageCollider();
        }
    }
}
