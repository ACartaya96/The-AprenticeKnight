using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    [CreateAssetMenu(menuName = "A.I./Enemy Actions/ States/ Attack")]
    public class AttackState : EnemyBaseState
    {
        public CombatStanceState combatStance;
        
        public EnemyAttackAction currentAttack;

        public override EnemyBaseState Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationHandler enemyAnimationHandler, FieldofView fov)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            float viewableAngle = Vector3.Angle(enemyManager.transform.forward, (enemyManager.currentTarget.transform.position - enemyManager.transform.position));
            if (enemyManager.isPerformingAction)
            {
                return combatStance;
            }
            else
            {
                if (currentAttack != null)
                {
                    //if we are too close to the enemy to perform current attac, get new attack
                    if (enemyManager.distanceFromTarget < currentAttack.minimumDistanceToAttack)
                    {
                        return this;
                    }
                    //if we are close enough to attack proceed to attack
                    else if (enemyManager.distanceFromTarget < currentAttack.maximumDistanceToAttack)
                    {
                        
                        //if our enemy is within our attacks viewable angle, we attack
                        if (viewableAngle < currentAttack.angle / 2)
                        {
                            //if the attack is viable, stop our movement and attack our target
                            if (enemyManager.currentRecoveryTime <= 0 && enemyManager.isPerformingAction == false)
                            {
                                enemyAnimationHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                                enemyAnimationHandler.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                                enemyAnimationHandler.PlayTargetAnimation(currentAttack.actionAnimation, true);
                                enemyManager.isPerformingAction = true;

                                //make a cooldown variable and set it to the attacks cooldown variable ( this gives player some time to recover if not the enemy will just switch from one attack the next with no remorse)
                                enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                                currentAttack = null;
                                //return to combat stance(this is always going to be the return state)
                                return combatStance;
                            }
                            else
                            {
                                return this;
                            }
                        }
                        else
                        {
                            return combatStance;
                        }
                    }
                    else
                    {
                        return combatStance;
                    }
                }
                else
                {
                    
                    return combatStance;
                }

            }
           
    
        }


    }
}
