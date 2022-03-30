using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(menuName = "A.I./Enemy Actions/ States/ Pursue")]
    public class PursueTargetState : EnemyBaseState
    {
        public override EnemyBaseState Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            //Chase the target
            //If we are in attack range (must be created), switch to Combat State
           if()

            return this;
        }
    }
    public void HandleMoveToTarget()//target can mean waypoints or player
    {
        if (enemyManager.isPerformingAction)
            return;

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
        distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);
        float viewAngle = Vector3.Angle(targetDirection, transform.forward);

        //If we are performing action(ex. Attack,Dodge,Spell,etc.) we turn of navmesh so that
        //the enemy can rotate towards its target with out worry of pathfinding.
        if(enemyManager.isPerformingAction)
        {
            enemyAnimationHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            navMeshAgent.enabled = false;
            
        }
        else
        {
            if(distanceFromTarget > stoppingDistance)
            {
                enemyAnimationHandler.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                
            }
            else if(distanceFromTarget <= stoppingDistance)
            {
                enemyAnimationHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }
            
        }
        
        HandleRotateTowardsTarget();
        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void HandleRotateTowardsTarget()
    {
        //Rotate Manually
        if(enemyManager.isPerformingAction)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if(direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed/Time.deltaTime);
        }
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = rb.velocity;

            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            rb.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
        }

        
    }
}
