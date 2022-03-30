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


        [SerializeField] public EnemyBaseState currentState;

       

        public bool isPerformingAction;

        public float currentRecoveryTime = 0;
        public float distanceFromTarget;
        public float stoppingDistance = 1;
        public float maximumAttackRange = 3;
        public float minimumAttackRange = 0;

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
            
               
        }

        private void SwitchToNextState(EnemyBaseState state)
        {
            currentState = state;
        }

      

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
   


    }
}
