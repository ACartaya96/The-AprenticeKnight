using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;
using System;

namespace TAK
{
    public class SwitchVCam : MonoBehaviour
    {
        public static SwitchVCam instance;

        [SerializeField]
        private int priorityBoostAmount = 10;



        [SerializeField] private Image aimReticle;

        public CinemachineFreeLook vcam;

        private void Awake()
        {
            vcam = GetComponent<CinemachineFreeLook>();

            if (instance != null && instance != this)
            {
                Destroy(this);
                return;
            }

            instance = this;
            vcam.m_RecenterToTargetHeading.m_enabled = false;
            aimReticle.enabled = false;
        }


        public void StartAim(Transform target)
        {
            vcam.LookAt = target;
            vcam.Priority = priorityBoostAmount;
            aimReticle.enabled = true;
        }

        public void CancelAim()
        {
            vcam.Priority = 9;
            aimReticle.enabled = false;
        }

    }
}
