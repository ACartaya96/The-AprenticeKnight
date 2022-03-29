using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    public Transform[] wayPoints;
    public int speed;
    private int wayPointIndex;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        wayPointIndex= 0;
        transform.LookAt(wayPoints[wayPointIndex].position); // face waypoint
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, wayPoints[wayPointIndex].position);
        if(distance < 1f) // range of 1
        {
            IncreaseIndex();
        }
        Patrol();
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        wayPointIndex++;
        if(wayPointIndex >= wayPoints.Length) //make sure index doesnt not go out of bounds
        {
            wayPointIndex = 0;
        }
        transform.LookAt(wayPoints[wayPointIndex].position);
    }
}
