using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(menuName = "A.I./Enemy Actions/ States/ Combat Stance")]
    public class CombatStanceState : EnemyBaseState
    {
        public AttackState attackState;
        public PursueTargetState pursueTarget;
        public override EnemyBaseState Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            //Check for attack range
            //if in attack range switch to attack State
            if (enemyManager.currentRecoveryTime <= 0 && enemyManager.distanceFromTarget <= enemyManager.maximumAttackRange)
            {
                return attackState;
            }
            //is the player runs out of ranger switch to the Pursue Target State
            else if (enemyManager.distanceFromTarget > enemyManager.maximumAttackRange)
            {
                return pursueTarget;
            }

            //if weare in a cooldown after attacking, return this state so that we continue to circle the player
            else
            {
                return this;
            }

            //circle player or walk around them until ready to attack player(WIP)

           
            

           
        }
    }
}
