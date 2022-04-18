﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class EnemyAnimationHandler : AnimationManager
    {
        EnemyManager enemyManager;
        public void Awake()
        {
            anim = GetComponent<Animator>();
            enemyManager = GetComponentInParent<EnemyManager>();
        }

        private void OnAnimatorMove()
        {
            enemyManager.rb.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition /Time.deltaTime;
            if (Time.timeScale != 0)
            {
                enemyManager.rb.velocity = velocity;
            }

            if(enemyManager.isRotatingWithRootMotion)
            {
                enemyManager.transform.rotation *= anim.deltaRotation;
            }
        }

    }    
}