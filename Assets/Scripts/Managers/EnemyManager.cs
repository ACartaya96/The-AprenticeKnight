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
        Animator anim;
        EnemyEffectManager enemyEffectManager;
       
        public bool isPerformingAction;
 
        [Header("State Machine")]
        [InlineEditor]
        [SerializeField] public EnemyBaseState currentState;

        [Header("Transform Targets")]
        public Transform castPoint;
        public CharacterManager currentTarget;

        [Header("Waypoint Navigation")]
        public Transform[] wayPoints;
        public int WayPointIndex;
        public float WaitTime;
        public Transform currentWayPoint;

        [Header("Movement Modifiers & Stats")]
        public NavMeshAgent navMeshAgent;
        public Rigidbody rb;
        public float stoppingDistance = 1;
        public float rotationSpeed = 25;

        [Header("Timers")]
        public float startWaitTime = 4f;
        public float currentRecoveryTime = 0;

        [Header("Entities Range")]
        public float distanceFromTarget;
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
            anim = GetComponentInChildren<Animator>();
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

            isRotatingWithRootMotion = enemyAnimationHandler.anim.GetBool("isRotatingWithRootMotion");
            isInteracting = anim.GetBool("isInteracting");
            canRotate = enemyAnimationHandler.anim.GetBool("canRotate");
            anim.SetBool("isInAir", isInAir);
            anim.SetBool("isBlocking", isBlocking);
            isPerformingAction = anim.GetBool("isPerformingAction"); 
            enemyAnimationHandler.canRotate = anim.GetBool("canRotate");
        }

        private void FixedUpdate()
        {
            HandleCurrentActionBehavior();
            enemyMovement.HandleFalling(enemyMovement.moveDirection);
            //enemyEffectManager.HandleAllBuildUpEffects();
            
        }

        private void LateUpdate()
        {
            if (isInAir)
            {
                enemyMovement.InAirTimer = enemyMovement.InAirTimer + Time.deltaTime;
            }
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
            if(isInteracting)
            {
                if(currentRecoveryTime <= 0)
                {
                    isInteracting = false;
                }
            }
        }
   


    }
}
