using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TAK
{
    public class EnemyMovement : MonoBehaviour
    {
        EnemyManager enemyManager;
        FieldofView fov;
        EnemyAnimationHandler enemyAnimationHandler;
       public NavMeshAgent navMeshAgent;

        public float distanceFromTarget;
        public float stoppingDistance = 1;
        public float rotationSpeed = 25;

        public Rigidbody rb;

        public float speed = 8;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            rb = GetComponent<Rigidbody>();
            fov = GetComponent<FieldofView>();
            enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        }

        private void Start()
        {
            navMeshAgent.enabled = false;
            rb.isKinematic = false;
        }


        public void HandleMoveToTarget()//target can mean waypoints or player
        {
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
                    targetDirection.Normalize();
                    targetDirection.y = 0;

                    targetDirection *= speed;

                    Vector3 projectedVelocity = Vector3.ProjectOnPlane(targetDirection, Vector3.up);
                    rb.velocity = projectedVelocity;

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
}
