using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float startWaitTime =4f;
    public float timeToRotate = 2f;
    public float speedWalk = 6f;
    public float speedRun = 9f;

    public float viewRadius = 15f;
    public float viewAngle = 90f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    public Transform[] wayPoints;
    int m_CurrentWayPointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 playerPosition;

    float m_WaitTime;
    float m_TImetoRotate;
    bool m_PlayerInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPLayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_TImetoRotate = timeToRotate;

        m_CurrentWayPointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(wayPoints[m_CurrentWayPointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        EnviromentView();
        if (!m_IsPatrol)
        {
            Chasing();
        }
        else
        {
            Patroling();
        }
    }

    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;

        if(!m_CaughtPLayer)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(playerPosition);
        }
        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if(m_WaitTime <= 0 &&  !m_CaughtPLayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                m_IsPatrol = true;
                m_PlayerNear= false;
                Move(speedWalk);
                m_TImetoRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(wayPoints[m_CurrentWayPointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                    m_WaitTime -=Time.deltaTime;
                }
            }
        }
    }

    private void Patroling()
    {
        if(m_PlayerNear)
        {
            if(m_TImetoRotate <=0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TImetoRotate-= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(wayPoints[m_CurrentWayPointIndex].position);
            if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if(m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    public void NextPoint()
    {
        m_CurrentWayPointIndex = (m_CurrentWayPointIndex + 1) % wayPoints.Length;
        navMeshAgent.SetDestination(wayPoints[m_CurrentWayPointIndex].position);
    }

    void CaughtPlayer ()
    {
        m_CaughtPLayer = true;
    }
    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if(Vector3.Distance(transform.position, player) <= 0.3f)
        {
            if(m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(wayPoints[m_CurrentWayPointIndex].position);
                m_WaitTime = startWaitTime;
                m_TImetoRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }
    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        for(int i=0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward,directionToPlayer)< viewAngle / 2)
            {
                float destinationToPlayer = Vector3.Distance(transform.position, player.position);
                if(!Physics.Raycast(transform.position, directionToPlayer, destinationToPlayer, obstacleMask))
                {
                    m_PlayerInRange = true;
                    m_IsPatrol = false;
                }
                else
                {
                    m_PlayerInRange = false;
                }
            }
            if(Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_PlayerInRange = false;
            }
        
            if(m_PlayerInRange)
            {
                playerPosition = player.transform.position;
            }
        }    
    }
}
