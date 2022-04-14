using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> waypoints;
    public float moveSpeed;
    public int target;
    float WaitTime;
    public float startWaitTime = 4f;
    // Update is called once per frame
    void Awake()
    {
        WaitTime = startWaitTime;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[target].position, moveSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        if(transform.position == waypoints[target].position)
        {
            if(WaitTime <= 0)
            {
                if(target == waypoints.Count - 1)
                {
                    target = 0;
                }
                else
                {
                    target += 1;
                }
                WaitTime = startWaitTime;
            }
            else
            {
                WaitTime -= Time.deltaTime;
            }

            
        }
    }
    IEnumerator waitHowEverlong()
    {
        yield return new WaitForSeconds(4f);
    }
}
