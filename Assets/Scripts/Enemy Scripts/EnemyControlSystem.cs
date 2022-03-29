using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TAK
{
    public class EnemyControlSystem : MonoBehaviour, IDamage
    {
        NavMeshAgent navMeshAgent;
        FieldofView fov;
        BoxCollider box;
        SpellClassType damageType;

        public Animator animator;
        //public Transform switchPos;
        RigidbodyConstraints rb;
        public Transform[] movePoints;
        public Transform playerLastPos;

        public EnemyManager enemyManager;
        public EnemyBaseState _state;
        public EnemyPatrolState PatrolState = new EnemyPatrolState();
        public EnemyChaseState ChaseState = new EnemyChaseState();
        public EnemySearchState SearchState = new EnemySearchState();
        public EnemyEnabled EnableState = new EnemyEnabled();
        public EnemyDisabled DisabledState = new EnemyDisabled();

        public float damage = 10f;

        public float maxHealth = 100;
        float currentHealth;

        private void Start()
        {
            _state = PatrolState;
            _state.EnterState(this);
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            box = GetComponent<BoxCollider>();
            fov = GetComponent<FieldofView>();
            enemyManager = GetComponentInChildren<EnemyManager>();
            currentHealth = maxHealth;
        }

        private void Update()
        {
            _state.UpdateState(this);
        }

        public void SwitchState(EnemyBaseState state)
        {
            _state = state;
            state.EnterState(this);
        }

        public void TakeDamage(float damage, string damageAnimation)
        {

            currentHealth -= damage;


            Debug.Log("Enemy HP: " + currentHealth.ToString());
            //animator.SetTrigger("Hurt");
            if (currentHealth <= 0)
            {
                Defeated();
            }
        }

        private void Defeated()
        {
            Destroy(gameObject);
        }

        
    }

        /*private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                if(fov.canSeePlayer && _state != DisabledState)
                    player.TakeDamage(this.damage);
            }
        }*/

    
}



