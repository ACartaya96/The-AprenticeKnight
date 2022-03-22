using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;
using System;

public class SwitchVCam : MonoBehaviour
{
    public InputHandler inputHandler;
    [SerializeField]
    private int priorityBoostAmount = 10;

    [SerializeField] private float maxLockOnDistance = 30;

    [SerializeField] private Image aimReticle;

    private CinemachineVirtualCamera virtualCamera;
    PlayerManager playerManager;

    bool lockedOn;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        lockedOn = true;
       
        aimReticle.enabled = false;
    }

    private void Update()
    {
        float shortestDistance = Mathf.Infinity;
        Collider[] colliders = Physics.OverlapSphere(virtualCamera.Follow.position, 26);

        foreach(Collider collider in colliders)
        {
            EnemyControlSystem enemy = collider.GetComponent<EnemyControlSystem>();

            if(enemy != null)
            {
                Vector3 lockTargetDirection = enemy.transform.position - virtualCamera.Follow.position;
                float distanceFromTarget = Vector3.Distance(virtualCamera.Follow.position, enemy.transform.position);
                float viewableAngle = Vector3.Angle(lockTargetDirection, virtualCamera.transform.forward);
                if(enemy.transform.root != virtualCamera.Follow.root && viewableAngle > -50 && viewableAngle < 50 
                    && distanceFromTarget <= maxLockOnDistance)
                {
                    virtualCamera.LookAt = enemy.transform;
                    if (inputHandler.rj_Input)
                        lockedOn = !lockedOn;
                    if (lockedOn == false)
                    {
                        
                        StartAim();
                      
                    }
                   else
                    {
                        CancelAim();
                    }
                    
                }
            }
        }
    }


    private void StartAim()
    {

        virtualCamera.Priority += priorityBoostAmount;
        aimReticle.enabled = true;
    }

    private void CancelAim()
    {
        virtualCamera.Priority = 9;
        aimReticle.enabled = false;
    }
}
