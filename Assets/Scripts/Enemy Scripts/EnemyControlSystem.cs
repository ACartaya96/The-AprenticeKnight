using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControlSystem : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    FieldofView fov;
    BoxCollider box;
    public Animator animator;
    //public Transform switchPos;
    RigidbodyConstraints rb;
    public Transform[] movePoints;
    public Transform playerLastPos;

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

    public void TakeDamage(float damage)
    {
        Debug.Log(damage.ToString());
        currentHealth -= damage;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.rb.AddForce(player.moveDirection * -100f, ForceMode.Impulse);
            player.TakeDamage(damage);
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


