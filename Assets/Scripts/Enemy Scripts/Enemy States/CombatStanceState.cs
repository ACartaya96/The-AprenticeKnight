using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(menuName = "A.I./Enemy Actions/ States/ Combat Stance")]
    public class CombatStanceState : EnemyBaseState
    {
        public AttackState attackState;
        public EnemyAttackAction[] enemyAttacks;
        public PursueTargetState pursueTarget;

        bool randomDestinationSet = false;
        float verticalMovementValue = 0;
        float horizontalMovementValue = 0;

        public override EnemyBaseState Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            enemyAnimationHandler.anim.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            enemyAnimationHandler.anim.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);
            if(enemyManager.isInteracting)
            {
                enemyAnimationHandler.anim.SetFloat("Vertical", 0);
                enemyAnimationHandler.anim.SetFloat("Horizontal", 0);
                return this;
            }
            //Check for attack range
            //if in attack range switch to attack State
    
            //is the player runs out of ranger switch to the Pursue Target State
            else if (enemyManager.distanceFromTarget > enemyManager.maximumAttackRange)
            {
                return pursueTarget;
            }

            if(!randomDestinationSet)
            {
                randomDestinationSet = true;
                //Decide How to walk around target
            }

            HandleRotateTowardsTarget(enemyManager);

            if (enemyManager.currentRecoveryTime <= 0 && attackState.currentAttack != null)
            {
                randomDestinationSet = false;
                return attackState;
            }
            //if weare in a cooldown after attacking, return this state so that we continue to circle the player
            else
            {
                GetNewAttack(enemyManager);
               
            }

            return this;

            //circle player or walk around them until ready to attack player(WIP)





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

        private void DecicedCirclingTarget(EnemyAnimationHandler enemyAnimationHandler)
        { 
        }

        private void WalkAroundTarget(EnemyAnimationHandler enemyAnimationHandler)
        {
            verticalMovementValue = Random.Range(0, 1);//alwats walking toward the player

            if(verticalMovementValue <=1 && verticalMovementValue > 0)
            {
                verticalMovementValue = 0.5f;
            }
            else if(verticalMovementValue >= -1 && verticalMovementValue < 0)
            {
                verticalMovementValue = -0.5f;
            }

            horizontalMovementValue = Random.Range(-1, 1);
            if(horizontalMovementValue <=1 && horizontalMovementValue >= 0)
            {
                horizontalMovementValue = 0.5f;
            }
            if(horizontalMovementValue >= -1 && horizontalMovementValue <= 0)
            {
                horizontalMovementValue = -0.5f;
            }
        }

        private void GetNewAttack(EnemyManager enemyManager)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position,
                enemyManager.transform.position);

            int maxScore = 0;

            foreach (EnemyAttackAction enemyAttack in enemyAttacks)
            {
                EnemyAttackAction enemyAttackAction = enemyAttack;

                //if the selected attack is not able to be use, for example: the attack selected doesn't have the range or the angle to the player. Get a new attack.
                if (enemyManager.distanceFromTarget <= enemyAttackAction.maximumDistanceToAttack
                    && enemyManager.distanceFromTarget >= enemyAttackAction.minimumDistanceToAttack)
                {

                    if (viewableAngle < enemyAttackAction.angle / 2)
                    {

                        maxScore += enemyAttackAction.attackScore;
                    }


                }
            }


            int randomValue = Random.Range(0, maxScore);
            int tempScore = 0;

            foreach (EnemyAttackAction enemyAttack in enemyAttacks)
            {
                EnemyAttackAction enemyAttackAction = enemyAttack;

                if (enemyManager.distanceFromTarget <= enemyAttackAction.maximumDistanceToAttack
                    && enemyManager.distanceFromTarget >= enemyAttackAction.minimumDistanceToAttack)
                {
                    if (viewableAngle < enemyAttackAction.angle / 2)
                    {

                        if (attackState.currentAttack != null)
                            return;

                        tempScore += enemyAttackAction.attackScore;

                        if (tempScore > randomValue)
                        {

                            attackState.currentAttack = enemyAttackAction;
                        }
                    }


                }
            }

        }
    }
}
