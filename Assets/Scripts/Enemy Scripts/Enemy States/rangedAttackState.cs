using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    [CreateAssetMenu(menuName = "A.I./Enemy Actions/ States/ Ranged Attack")]
    public class rangedAttackState : EnemyBaseState
    {
        public CombatStanceState combatStance;
        public EnemyIdleState idleState;
        

        public override EnemyBaseState  Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            float viewableAngle = Vector3.Angle(enemyManager.transform.forward, (enemyManager.currentTarget.transform.position - enemyManager.transform.position));


            if (enemyManager.isPerformingAction)
            {
                return this;
            }
            else
            {
                if (enemyManager.distanceFromTarget >= 15f)
                {
                    //preform ranged attack
                    return this;
                }
                return this;
            }
           
    
        }
    }
}

