using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{

    public class EnemyAttack : MonoBehaviour
    {
        public DamageCollider rightDamageCollider;
        public DamageCollider leftDamageCollider;
        public DamageCollider bodyDamageCollider;

  
        public void OpenRightDamageCollider()
        {
            rightDamageCollider.EnableDamageCollider();
        }
        public void OpenLeftDamageCollider()
        {
            leftDamageCollider.EnableDamageCollider();
        }
        public void CloseRightDamageCollider()
        {
            rightDamageCollider.DisableDamageCollider();
        }
        public void CloseLeftDamageCollider()
        {
            leftDamageCollider.DisableDamageCollider();
        }

        public void OpenBodyDamageCollider()
        {
            bodyDamageCollider.EnableDamageCollider();
        }

        public void CloseBodyDamageCollider()
        {
            bodyDamageCollider.EnableDamageCollider();
        }

    }
}
