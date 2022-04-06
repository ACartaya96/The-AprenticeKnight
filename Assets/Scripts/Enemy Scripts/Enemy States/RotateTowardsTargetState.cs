using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(menuName = "A.I./Enemy Actions/ States/ Rotate Towards State")]
    public class RotateTowardsTargetState : EnemyBaseState
    {
        CombatStanceState combatStance;

        public override EnemyBaseState Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            enemyAnimationHandler.anim.SetFloat("Vertical", 0);
            enemyAnimationHandler.anim.SetFloat("Horizontal", 0);

            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);

            if(viewableAngle >= 100 && viewableAngle <=100 && !enemyManager.isInteracting)
            {
                enemyAnimationHandler.PlayTargetAnimationWithRootRotation("Turn Behind", true);
                return this;
            }

            return this;
        }
    }
}
