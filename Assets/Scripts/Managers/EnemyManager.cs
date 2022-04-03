using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

namespace TAK
{
    public class EnemyManager : CharacterManager
    {
        FieldofView fov;
        EnemyMovement enemyMovement;
        EnemyAnimationHandler enemyAnimationHandler;
        EnemyStats enemyStats;
        public CharacterManager currentTarget;
        EnemyEffectManager enemyEffectManager;

        [InlineEditor]
        public NavMeshAgent navMeshAgent;
        public int WayPointIndex;
        public float WaitTime;
        public float startWaitTime = 4f;

        
        
        public Rigidbody rb;

        [InlineEditor]
        [SerializeField] public EnemyBaseState currentState;

        public Transform castPoint;

        public Transform[] wayPoints;
        public Transform currentWayPoint;

        public float distanceFromTarget;
        public float stoppingDistance = 1;
        public float rotationSpeed = 25;

        public bool isPerformingAction;

        public float currentRecoveryTime = 0;
        public float maximumAttackRange = 3;
        public float minimumAttackRange = 0;

        // Start is called before the first frame update
        private void Awake()
        {
            fov = GetComponent<FieldofView>();
            enemyMovement = GetComponent<EnemyMovement>();
            enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();
            enemyStats = GetComponent<EnemyStats>();
            navMeshAgent= GetComponentInChildren<NavMeshAgent>();
            enemyEffectManager = GetComponentInChildren<EnemyEffectManager>();
            rb = GetComponent<Rigidbody>();
            WayPointIndex = 0;
            
            WaitTime = startWaitTime;
            navMeshAgent.enabled = false; 
        }

        // Update is called once per frame
        void Update()
        {
            HandleRecoveryTimer();

            isInteracting = enemyAnimationHandler.anim.GetBool("isInteracting");
        }

        private void FixedUpdate()
        {
            HandleCurrentActionBehavior();
            enemyEffectManager.HandleAllBuildUpEffects();
            
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
