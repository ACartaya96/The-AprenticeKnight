using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetDetection : MonoBehaviour
{
    PlayerManager playerManager;
    InputHandler inputHandler;
    Camera cam;

    public float maxLockOnDistance;
    List<CharacterManager> availableTargets = new List<CharacterManager>();

    public Transform nearestLockOnTarget;
    public  Transform targetTransform;
    public Transform leftLockOnTarget;
    public Transform rightLockOnTarget;

    Transform originalPosition;



    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        inputHandler = GetComponent<InputHandler>();
        originalPosition = playerManager.cameraTarget;
        cam = Camera.main;
    }

    private void Update()
    {
        HandleCamera();

    }

    private void HandleCamera()
    {
        if(inputHandler.lockedOnflag == false)
        {
            
            SwitchVCam.instance.CancelAim();
            ClearLockOnTarget();
        }
        else
        {
            playerManager.cameraTarget = playerManager.currentLockedOnTarget;
            SwitchVCam.instance.StartAim(playerManager.currentLockedOnTarget);
        }
    }
    public void HandleLockOn()
    {
        float shortestDistance = Mathf.Infinity;
        float shortestDistanceOfLeftTarget = Mathf.Infinity;
        float shortestDistanceOfRightTarget = Mathf.Infinity;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 26);

        //Debug.Log(colliders.ToString());

        foreach (Collider collider in colliders)
        {
            CharacterManager character = collider.GetComponent<CharacterManager>();

            //Debug.Log(character.name.ToString());

            if (character != null)
            {
                Vector3 lockTargetDirection = character.transform.position - targetTransform.transform.position;
                float distanceFromTarget = Vector3.Distance(targetTransform.transform.position, character.transform.position);
                float viewableAngle = Vector3.Angle(lockTargetDirection, cam.transform.forward);
                if (character.transform.root != targetTransform.transform.root && viewableAngle > -50 && viewableAngle < 50
                    && distanceFromTarget <= maxLockOnDistance)
                {

                    availableTargets.Add(character);
                    Debug.Log(availableTargets.ToString());
                    /*if (inputHandler.rj_Input)
                        playerManager.lockedOn = !playerManager.lockedOn;

                    if (playerManager.lockedOn == false)
                    {
                        playerManager.LockedOntarget = character.transform;
                        playerManager.cameraTarget = playerManager.LockedOntarget;
                        SwitchVCam.instance.StartAim();
                    }
                    else
                    {
                        SwitchVCam.instance.CancelAim();
                        playerManager.cameraTarget = originalPosition;   
                    }*/

                }
            }
        }

        foreach(CharacterManager availableTarget in availableTargets)
        {
            float distanceFromTarget = Vector3.Distance(transform.position, availableTarget.transform.position);
            if(distanceFromTarget < shortestDistance)
            {
                shortestDistance = distanceFromTarget;
                nearestLockOnTarget = availableTarget.LockOnTransform;
            }

            if(inputHandler.lockedOnflag)
            {
                Vector3 relativeEnemyPosition = playerManager.currentLockedOnTarget.InverseTransformPoint(availableTarget.transform.position);
                var distanceFromLeftTarget = playerManager.currentLockedOnTarget.transform.position.x - availableTarget.transform.position.x;
                var distanceFromRightTarget = playerManager.currentLockedOnTarget.transform.position.x + availableTarget.transform.position.x;

                if(relativeEnemyPosition.x > 0.00 && distanceFromLeftTarget < shortestDistanceOfLeftTarget)
                {
                    shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                    leftLockOnTarget = availableTarget.LockOnTransform;
                }

                if (relativeEnemyPosition.x < 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget)
                {
                    shortestDistanceOfRightTarget = distanceFromRightTarget;
                    rightLockOnTarget = availableTarget.LockOnTransform;
                }
            }
        }
    }

    public void ClearLockOnTarget()
    {
        availableTargets.Clear();
        nearestLockOnTarget = null;
        playerManager.currentLockedOnTarget = null;
        
    }

}
