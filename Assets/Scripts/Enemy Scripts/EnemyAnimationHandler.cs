using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class EnemyAnimationHandler : AnimationManager
    {
        EnemyMovement enemyMovement;
        public void Awake()
        {
            anim = GetComponent<Animator>();
            enemyMovement = GetComponentInParent<EnemyMovement>();
        }

        private void OnAnimatorMove()
        {
            enemyMovement.rb.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition /Time.deltaTime;
            enemyMovement.rb.velocity = velocity;
        }

    }    
}