
using UnityEngine;
using UnityEngine.AI;

public class EnemySearchState : EnemyBaseState
{
    private NavMeshAgent navMeshAgent;
    FieldofView fov;
    GameObject player;
    public int randomSpot;

    public int startTime = 30;
    public float countdown;

    public override void EnterState(EnemyControlSystem enemy)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fov = enemy.GetComponent<FieldofView>();
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();

        enemy.animator.SetBool("CanSeePlayer", false);
        enemy.animator.SetBool("isSearching", true);

        countdown = startTime;
    }
    public override void UpdateState(EnemyControlSystem enemy)
    {
        if (fov.canSeePlayer)
        {
            Debug.Log(enemy.name + ": 8108 has been detected");
            
            enemy.SwitchState(enemy.ChaseState);
        }
        else if (countdown >= 0)
        {
            Searching(enemy);
        }
        else
        {
            Debug.Log(enemy.name + ": 8108 has been lost continue Patrol.");
            
          
            
            enemy.SwitchState(enemy.PatrolState);
        }
        //Debug.Log(countdown.ToString());
        countdown -= Time.deltaTime;
    }
    public override void OnCollisionEnter(EnemyControlSystem enemy)
    {

    }

    void Searching(EnemyControlSystem enemy)
    {
        if (navMeshAgent.remainingDistance <= 0)
        {
            navMeshAgent.destination = (Random.insideUnitSphere * 10.0f) + enemy.playerLastPos.position;
            //Debug.Log(navMeshAgent.destination.ToString());
        }
    }
}
