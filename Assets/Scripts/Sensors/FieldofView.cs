using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldofView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;
    public float heightCap = 4.0f;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public bool canHearPlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOFViewCheck();
        }

        
    }

    private void FieldOFViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            
            if(Vector3.Angle(transform.forward, directionToTarget) < angle/2)
            {
                float distancetoTarget = Vector3.Distance(transform.position, target.position);

                if (playerRef.transform.position.y < radius / heightCap)
                {
                    if (!Physics.Raycast(transform.position, directionToTarget, distancetoTarget, obstructionMask))
                    {
                        canSeePlayer = true;
                    }
                    else
                    {
                        canSeePlayer = false;
                    }
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if(canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    public void ReportCanHear(Vector3 location, EHeardSoundCategory category, float intesity)
    {
        Debug.Log("Heard sound " + category + " at " + location.ToString() + " with intensity of " + intesity);

    }
}
