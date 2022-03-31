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
            fov.FieldOFViewCheck();
            if (enemyManager.currentTarget != null)
            {
                //Switch to a pursue target state if it finds a target
                enemyManager.navMeshAgent.enabled = false;
                return pursueTargetState;
            }
            else
            {
                enemyManager.navMeshAgent.enabled = true;
                currentWayPoint = wayPoints[enemyManager.WayPointIndex];
                enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.transform.position , wayPoints[enemyManager.WayPointIndex].position);
                Vector3 relativeDirection = enemyManager.transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyManager.rb.velocity;

                enemyManager.navMeshAgent.enabled = true;
                enemyManager.navMeshAgent.SetDestination(currentWayPoint.position);
                enemyManager.rb.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);

                enemyAnimationHandler.anim.SetFloat("Vertical", 0.5f, 0.1f, Time.deltaTime);
                if(  enemyManager.distanceFromTarget <=   enemyManager.stoppingDistance)
                {
                    if(enemyManager.WaitTime <= 0)
                    {
                        NextPoint(enemyManager);
                        Move(enemyManager,speedWalk);
                        enemyManager.WaitTime = enemyManager.startWaitTime;
                    }
                    else
                    {
                       
                        Stop(enemyManager);
                        enemyAnimationHandler.anim.SetFloat("Vertical", 0f, 0.01f, Time.deltaTime);
                        
                        enemyManager.WaitTime -= Time.deltaTime;
                    }
                    
                }
                return this;
            }
            
            
        }
        void Move(EnemyManager enemyManager,float speed)
        {
            enemyManager.navMeshAgent.isStopped = false;
            enemyManager.navMeshAgent.speed = speed;
        }

        void Stop(EnemyManager enemyManager)
        {

            enemyManager.navMeshAgent.isStopped = true;
            enemyManager.navMeshAgent.speed = 0;
        }

        void NextPoint(EnemyManager enemyManager)
        {
            enemyManager.WayPointIndex = (enemyManager.WayPointIndex + 1) % wayPoints.Length;
            enemyManager.navMeshAgent.SetDestination(wayPoints[enemyManager.WayPointIndex].position);
        }
    }
}
