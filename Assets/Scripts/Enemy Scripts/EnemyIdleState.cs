using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TAK
{
    [CreateAssetMenu(menuName = "A.I./Enemy Actions/ States/ Idle")]
    public class EnemyIdleState : EnemyBaseState
    {
        public PursueTargetState pursueTargetState;
        
        public float speedWalk = 4.5f;
        public Transform[] wayPoints;
        public Transform currentWayPoint;
        
        public override EnemyBaseState Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            //Move its partol route


            
            //Look for a potential target
           
            if (enemyManager.isPerformingAction)
                return this;

            fov.FieldOFViewCheck();

            Vector3 targetDirection = wayPoints[enemyManager.WayPointIndex].position - enemyManager.transform.position;
            float distanceFromTarget = Vector3.Distance(wayPoints[enemyManager.WayPointIndex].position, enemyManager.transform.position);
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
                if (distanceFromTarget > enemyManager.maximumAttackRange)
                {
                    enemyAnimationHandler.anim.SetFloat("Vertical", 0.5f, 0.1f, Time.deltaTime);

                }
                else if (distanceFromTarget <= enemyManager.maximumAttackRange)
                {
                    enemyAnimationHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);

                }

            }
            HandleRotateTowardsTarget(enemyManager);
            enemyManager.navMeshAgent.nextPosition = enemyManager.transform.position;
            enemyManager.navMeshAgent.transform.localRotation = Quaternion.identity;

            if(distanceFromTarget <= enemyManager.maximumAttackRange)
            {
                if(enemyManager.WaitTime <= 0)
                {
                    enemyManager.WayPointIndex = (enemyManager.WayPointIndex + 1) % wayPoints.Length;
                    currentWayPoint = wayPoints[enemyManager.WayPointIndex];
                    enemyManager.navMeshAgent.speed = speedWalk;
                }
                else
                {
                   
                    enemyManager.navMeshAgent.speed = 0;
                    enemyManager.WaitTime -= Time.deltaTime;
                }
            }
            //If we are in attack range (must be created), switch to Combat State
            if (enemyManager.currentTarget != null)
            {
                enemyManager.navMeshAgent.ResetPath();
                return pursueTargetState;
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
                Vector3 direction = wayPoints[enemyManager.WayPointIndex].position - enemyManager.transform.position;
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
                enemyManager.navMeshAgent.SetDestination(currentWayPoint.position);
                enemyManager.rb.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            }



         
        }
            
            
    }
       

     
}

