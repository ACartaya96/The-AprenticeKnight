using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(menuName = "A.I./Enemy Actions/ States/ Rotate Towards State")]
    public class RotateTowardsTargetState : EnemyBaseState
    {
        public CombatStanceState combatStance;
        public PursueTargetState pursueTarget;
        public float minAngle;
        public float maxAngle;

        public override EnemyBaseState Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            enemyAnimationHandler.anim.SetFloat("Vertical", 0);
            enemyAnimationHandler.anim.SetFloat("Horizontal", 0);

            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);

            if(enemyManager.isInteracting)
            {
                return this;
            }

            if(viewableAngle >= 100 && viewableAngle <= 180 && !enemyManager.isInteracting)
            {
                enemyAnimationHandler.PlayTargetAnimationWithRootRotation("Turn Behind", true);
                return pursueTarget;
            }
            else if(viewableAngle <= -101 && viewableAngle >= -180 && !enemyManager.isInteracting)
            {
                enemyAnimationHandler.PlayTargetAnimationWithRootRotation("Turn Behind", true);
                return pursueTarget;
            }
            else if(viewableAngle <= -45 && viewableAngle >= -100 && !enemyManager.isInteracting)
            {
                enemyAnimationHandler.PlayTargetAnimationWithRootRotation("Right Turn", true);
                return pursueTarget;
            }
            else if(viewableAngle >= 45 && viewableAngle <= 100 && !enemyManager.isInteracting)
            { 
                enemyAnimationHandler.PlayTargetAnimationWithRootRotation("Left Turn", true);
                return pursueTarget;
            }

            return pursueTarget;
        }
    }
}
