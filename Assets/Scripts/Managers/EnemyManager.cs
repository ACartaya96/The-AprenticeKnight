using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    public class EnemyManager : CharacterManager
    {
        FieldofView fov;
        EnemyMovement enemyMovement;
        EnemyAnimationHandler enemyAnimationHandler;
        EnemyStats enemyStats;

        public CharacterManager currentTarget;

        public EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttack;

        [SerializeField] public EnemyBaseState currentState;

       

        public bool isPerformingAction;

        public float currentRecoveryTime = 0;

        // Start is called before the first frame update
        private void Awake()
        {
            fov = GetComponent<FieldofView>();
            enemyMovement = GetComponent<EnemyMovement>();
            enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();
            enemyStats = GetComponent<EnemyStats>();

        }

        // Update is called once per frame
        void Update()
        {
            HandleRecoveryTimer();
        }

        private void FixedUpdate()
        {
            HandleCurrentActionBehavior();
            
        }

        private void HandleCurrentActionBehavior()
        {
            if(currentState != null)
            {
                EnemyBaseState nextState = currentState.Tick(this, enemyStats, enemyAnimationHandler, fov);
                
                if(nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
            //if(currentTarget != null)
            //    enemyMovement.distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
            
            //if (currentTarget == null)
            //{
            //    fov.FieldOFViewCheck();
            //}
               
            //else if (enemyMovement.distanceFromTarget > enemyMovement.stoppingDistance)
            //{
            //    Debug.Log(name.ToString() + " Moving to " + currentTarget.name.ToString());
            //    enemyMovement.HandleMoveToTarget();
            //}
                
            //else if (enemyMovement.distanceFromTarget <= enemyMovement.stoppingDistance)
            //{
            //    Debug.Log(name.ToString() + " Attacking " + currentTarget.name.ToString());

            //    AttackTarget();
            //}
               
        }

        private void SwitchToNextState(EnemyBaseState state)
        {
            currentState = state;
        }

        #region ATTACK
        //private void AttackTarget()
        //{
        //    if (isPerformingAction)
        //        return;

        //    if(currentAttack == null)
        //    {
        //        Debug.Log("Need New Attack Command.");
        //        GetNewAttack();
        //    }
        //    else
        //    {
        //        Debug.Log("Attacking with " + currentAttack.actionAnimation.ToString());
        //        isPerformingAction = true;
        //        currentRecoveryTime = currentAttack.recoveryTime;
        //        enemyAnimationHandler.PlayTargetAnimation(currentAttack.actionAnimation, true);
        //        currentAttack = null;
        //    }
        //}

        //private void GetNewAttack()
        //{
        //    Vector3 targetDirection = currentTarget.transform.position - transform.position;
        //    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        //    enemyMovement.distanceFromTarget = Vector3.Distance(currentTarget.transform.position,
        //        transform.position);

        //    int maxScore = 0;

        //    foreach(EnemyAttackAction enemyAttack in enemyAttacks)
        //    {
        //        EnemyAttackAction enemyAttackAction = enemyAttack;

        //        if(enemyMovement.distanceFromTarget <= enemyAttackAction.maximumDistanceToAttack 
        //            && enemyMovement.distanceFromTarget >= enemyAttackAction.minimumDistanceToAttack)
        //        {
        //            Debug.Log("Gate 1");
        //            if (viewableAngle < enemyAttackAction.angle / 2)
        //            {
        //                Debug.Log("Gate 2");
        //                maxScore += enemyAttackAction.attackScore;
        //            }

                   
        //        }
        //    }

        //    int randomValue = Random.Range(0, maxScore);
        //    int tempScore = 0;

        //    foreach (EnemyAttackAction enemyAttack in enemyAttacks)
        //    {
        //        EnemyAttackAction enemyAttackAction = enemyAttack;

        //        if (enemyMovement.distanceFromTarget <= enemyAttackAction.maximumDistanceToAttack
        //            && enemyMovement.distanceFromTarget >= enemyAttackAction.minimumDistanceToAttack)
        //        {
        //            Debug.Log("Gate 1B");
        //            if (viewableAngle < enemyAttackAction.angle / 2)
        //            {
        //                Debug.Log("Gate 2B");
        //                if (currentAttack != null)
        //                    return;
        //                Debug.Log("Gate 3B");
        //                tempScore += enemyAttackAction.attackScore;

        //                if(tempScore > randomValue)
        //                {
        //                    Debug.Log("Gate 4B");
        //                    currentAttack = enemyAttackAction;
        //                }
        //            }


        //        }
        //    }
        //}

        private void HandleRecoveryTimer()
        {
            if(currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }
            if(isPerformingAction)
            {
                if(currentRecoveryTime <= 0)
                {
                    isPerformingAction = false;
                }
            }
        }
        #endregion


    }
}
