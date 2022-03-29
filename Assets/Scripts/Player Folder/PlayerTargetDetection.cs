using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class PlayerTargetDetection : MonoBehaviour
    {
        PlayerManager playerManager;
        InputHandler inputHandler;
        Camera cam;

        [Header("Lock On Modifiers")]
        public float maxLockOnDistance;
        public LayerMask obstructionLayer;
        List<CharacterManager> availableTargets = new List<CharacterManager>();

        [Header("Locked On Targets")]
        public Transform nearestLockOnTarget;
        public Transform currentLockedOnTarget;
        public Transform leftLockOnTarget;
        public Transform rightLockOnTarget;
        public Transform targetTransform;


        Transform originalPosition;
        Transform target;



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
            if (inputHandler.lockedOnflag == false)
            {

                SwitchVCam.instance.CancelAim();
                ClearLockOnTarget();
            }
            else
            {

                SwitchVCam.instance.StartAim(currentLockedOnTarget);
            }
        }
        public void HandleLockOn()
        {
            float shortestDistance = Mathf.Infinity;
            float shortestDistanceOfLeftTarget = -Mathf.Infinity;
            float shortestDistanceOfRightTarget = Mathf.Infinity;
            Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);

            foreach (Collider collider in colliders)
            {
                CharacterManager character = collider.GetComponent<CharacterManager>();

                

                if (character != null)
                {
                    Vector3 lockTargetDirection = character.transform.position - targetTransform.transform.position;
                    float distanceFromTarget = Vector3.Distance(targetTransform.transform.position, character.transform.position);
                    float viewableAngle = Vector3.Angle(lockTargetDirection, cam.transform.forward);
                    RaycastHit hit;
                    if (character.transform.root != targetTransform.transform.root && viewableAngle > -50 && viewableAngle < 50
                        && distanceFromTarget <= maxLockOnDistance)
                    {
                        if (Physics.Linecast(cam.transform.position, character.LockOnTransform.position, out hit, obstructionLayer))
                        {
                            Debug.DrawLine(cam.transform.position, character.LockOnTransform.position);


                        }
                        else
                        {
                            availableTargets.Add(character);
                        }
                    }
                }
            }

            foreach (CharacterManager availableTarget in availableTargets)
            {
                float distanceFromTarget = Vector3.Distance(transform.position, availableTarget.transform.position);
                if (distanceFromTarget < shortestDistance)
                {
                    shortestDistance = distanceFromTarget;
                    nearestLockOnTarget = availableTarget.LockOnTransform;
                }

                if (inputHandler.lockedOnflag)
                {
                    //Vector3 relativeEnemyPosition = playerManager.currentLockedOnTarget.transform.InverseTransformPoint(availableTarget.transform.position);
                    //var distanceFromLeftTarget = playerManager.currentLockedOnTarget.transform.position.x - availableTarget.transform.position.x;
                    //var distanceFromRightTarget = playerManager.currentLockedOnTarget.transform.position.x + availableTarget.transform.position.x;
                    Vector3 relativeEnemyPosition = inputHandler.transform.InverseTransformPoint(availableTarget.transform.position);
                    var distanceFromLeftTarget = relativeEnemyPosition.x;
                    var distanceFromRightTarget = relativeEnemyPosition.x;

                    if (relativeEnemyPosition.x <= 0.00 && distanceFromLeftTarget > shortestDistanceOfLeftTarget
                        && currentLockedOnTarget.root != availableTarget.LockOnTransform.root)
                    {
                        shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                        leftLockOnTarget = availableTarget.LockOnTransform;
                    }
                    else if (relativeEnemyPosition.x >= 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget
                        && currentLockedOnTarget.root != availableTarget.LockOnTransform.root)
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
            currentLockedOnTarget = null;
            leftLockOnTarget = null;
            rightLockOnTarget = null;

        }

    }
}
