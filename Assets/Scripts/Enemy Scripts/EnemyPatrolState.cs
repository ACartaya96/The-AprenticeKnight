
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    public float speed;

    
    private NavMeshAgent navMeshAgent;
    FieldofView fov;
    public int randomSpot;

    public int startTime = 3;
    public float countdown;

    public override void EnterState(EnemyControlSystem enemy)
    {
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        fov = enemy.GetComponent<FieldofView>();
        randomSpot = Random.Range(0, enemy.movePoints.Length);

        enemy.animator.SetBool("isSearching", false);
        enemy.animator.SetBool("CanSeePlayer", false);

        countdown = startTime;
    }
    public override void UpdateState(EnemyControlSystem enemy)
    {
        if (fov.canSeePlayer)
        {
            Debug.Log(enemy.name + ": 8108 has been detected");
           
            enemy.SwitchState(enemy.ChaseState);
        }
        navMeshAgent.destination = enemy.movePoints[randomSpot].position;
        Pathfinding(enemy);
    }
    public override void OnCollisionEnter(EnemyControlSystem enemy)
    {

    }

    void Pathfinding(EnemyControlSystem enemy)
    {
        if (navMeshAgent.remainingDistance <= 0)
        {
           
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                
                randomSpot = Random.Range(0, enemy.movePoints.Length);
                
                countdown = startTime;
            }
         
        }
    }

}
