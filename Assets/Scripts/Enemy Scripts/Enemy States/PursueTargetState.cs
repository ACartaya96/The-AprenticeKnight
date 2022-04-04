using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TAK
{
    
    [CreateAssetMenu(menuName = "A.I./Enemy Actions/ States/ Pursue")]
    public class PursueTargetState : EnemyBaseState
    {
        public CombatStanceState combatStance;
        public override EnemyBaseState Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            //Chase the target
            if (enemyManager.isPerformingAction)
            {
                enemyAnimationHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);

                return this;
            }

            enemyManager.navMeshAgent.speed = 8;
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            float viewAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

            //If we are performing action(ex. Attack,Dodge,Spell,etc.) we turn of navmesh so that
            //the enemy can rotate towards its target with out worry of pathfinding.
            if (enemyManager.isPerformingAction)
            {
                enemyAnimationHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                enemyManager.navMeshAgent.enabled = false;
               
            }
            else
            {
                if (enemyManager.distanceFromTarget > enemyManager.stoppingDistance)
                {
                    enemyAnimationHandler.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                   
                }
                else if (enemyManager.distanceFromTarget <= enemyManager.stoppingDistance)
                {
                    enemyAnimationHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                  
                }

            }

            HandleRotateTowardsTarget(enemyManager);
            enemyManager.navMeshAgent.transform.localPosition = Vector3.zero;
            enemyManager.navMeshAgent.transform.localRotation = Quaternion.identity;
            //If we are in attack range (must be created), switch to Combat State
            if(enemyManager.distanceFromTarget <= enemyManager.maximumAttackRange)
            {
                return combatStance;
            }
            else
            {
                return this;
            }
            
        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            //Rotate Manually
            if (enemyManager.isPerformingAction)
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = enemyManager.transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
            else
            {
                Vector3 relativeDirection = enemyManager.transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyManager.rb.velocity;

                enemyManager.navMeshAgent.enabled = true;
                enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.rb.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            }


        }
    }
    
    
}
