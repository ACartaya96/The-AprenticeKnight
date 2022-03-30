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
            //playerLastPosition = Vector3.zero;

            
            //Look for a potential target
            fov.FieldOFViewCheck();
            if (enemyManager.currentTarget != null)
            {
                //Switch to a pursue target state if it finds a target
                return pursueTargetState;
            }
            else
            {
                currentWayPoint = wayPoints[enemyManager.WayPointIndex];
                enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.transform.position , wayPoints[enemyManager.WayPointIndex].position);
                enemyManager.navMeshAgent.SetDestination(currentWayPoint.position);
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
                        enemyAnimationHandler.anim.SetFloat("Vertical", 0f, 0.1f, Time.deltaTime);
                        Stop(enemyManager);
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
