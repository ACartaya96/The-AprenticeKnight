using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace TAK
{
    public class PlayerController : MonoBehaviour
    {


        Transform cameraObject;
        InputHandler inputHandler;
        PlayerManager playerManager;
        PlayerTargetDetection playerTarget;
        public Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public AnimationHandler animationHandler;

        public Rigidbody rb;
        public GameObject normalCamera;

        [Header("Ground & Air Stats")]
        [SerializeField]
        float groundDetectionRayStartPoint = 0.5f;
        [SerializeField]
        float minimumDistanceNeededTobeginFall = 1f;
        [SerializeField]
        float groundDirectionRayDistance = 0.2f;
        [SerializeField]
        LayerMask ignoreforGrounCheck;
        public float InAirTimer;

        [Header("Movement Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float rotationSpeed = 10;
        [SerializeField]
        float rollVelocity = 50;
        [Space]
        [SerializeField]
        float fallSpeed = 45f;
        [SerializeField]
        float jumpUpwardVelocity = 50;
        [SerializeField]
        float jumpForwardVelocity = 50;
        [SerializeField]
        float rollUpwardVelocity = 50;
        [SerializeField]
        float rollForwardVelocity = 50;

        public bool jumpForceApplied;
        public bool rollForceApplied;


        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlockeCollider;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            inputHandler = GetComponentInChildren<InputHandler>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
            playerManager = GetComponentInChildren<PlayerManager>();
            playerTarget = GetComponentInChildren<PlayerTargetDetection>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            ignoreforGrounCheck = ~ignoreforGrounCheck;
            animationHandler.Initialize();

            Physics.IgnoreCollision(characterCollider,characterCollisionBlockeCollider, true);

            playerManager.isGrounded = true;
        }

        private void FixedUpdate()
        {

            if (jumpForceApplied)
            {
                StartCoroutine(JumpCo());
                rb.AddForce(0, jumpUpwardVelocity, 0);
                rb.AddForce(0, 0, jumpForwardVelocity);
            }
            else if (rollForceApplied)
            {
                StartCoroutine(RollCo());
                rb.AddForce(myTransform.up * rollUpwardVelocity);
                rb.AddForce(myTransform.forward * rollForwardVelocity);

            }
        }
        private IEnumerator JumpCo()
        {
            yield return new WaitForSeconds(0.65f);
            jumpForceApplied = false;

        }

        private IEnumerator RollCo()
        {
            yield return new WaitForSeconds(0.55f);
            rollForceApplied = false;
        }


        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;
        public void HandleMovement()
        {
            if (inputHandler.rollflag)
                return;

            if (playerManager.isInteracting)
                return;

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            moveDirection *= movementSpeed;



            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rb.velocity = projectedVelocity;

            if (inputHandler.lockedOnflag)
            {
                animationHandler.UpdateAnimatorValues(inputHandler.vertical, inputHandler.horizontal);
            }
            else
            {
                animationHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);
            }


          
        }
        public void HandleRotation()
        {
            if (animationHandler.canRotate)
            {
                if (inputHandler.lockedOnflag)
                {
                    if (inputHandler.rollflag)
                    {

                        Vector3 targetDirection = Vector3.zero;
                        targetDirection = cameraObject.transform.forward * inputHandler.vertical;
                        targetDirection += cameraObject.transform.right * inputHandler.horizontal;
                        targetDirection.y = 0;

                        if (targetDirection == Vector3.zero)
                            targetDirection = transform.forward;


                        Quaternion tr = Quaternion.LookRotation(targetDirection);
                        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rotationSpeed * 2 * Time.deltaTime);

                        myTransform.rotation = targetRotation;
                    }
                    else
                    {
                        Vector3 rotationDirection = moveDirection;
                        rotationDirection = playerTarget.currentLockedOnTarget.transform.position - myTransform.position;
                        rotationDirection.y = 0;
                        rotationDirection.Normalize();

                        Quaternion tr = Quaternion.LookRotation(rotationDirection);
                        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rotationSpeed * Time.deltaTime);

                        myTransform.rotation = targetRotation;
                    }
                }
                else
                {
                    Vector3 targetDir = Vector3.zero;
                    float moveOverride = inputHandler.moveAmount;

                    targetDir = cameraObject.forward * inputHandler.vertical;
                    targetDir += cameraObject.right * inputHandler.horizontal;

                    targetDir.Normalize();
                    targetDir.y = 0;


                    if (targetDir == Vector3.zero)
                        targetDir = myTransform.forward;

                    Quaternion tr = Quaternion.LookRotation(targetDir);
                    Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rotationSpeed * Time.deltaTime);

                    myTransform.rotation = targetRotation;

                }


            }
           
           
        }

        public void HandleRollingandSprinting()
        {
            if (animationHandler.anim.GetBool("isInteracting"))
                return;

            if (inputHandler.rollflag)
            {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;
                rb.AddForce(moveDirection * rollVelocity * Time.deltaTime, ForceMode.Impulse);
                rollForceApplied = true;
                if (inputHandler.moveAmount > 0)
                {
                    animationHandler.PlayTargetAnimation("Rolling", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;

                }
                /* else 
                 {
                     animationHandler.PlayTargetAnimation("Backstep", true);
                 }*/

            }
        }

        public void HandleJumping()
        {
            if (animationHandler.anim.GetBool("isInteracting"))
                return;

            if (animationHandler.anim.GetBool("isInAir"))
                return;

            if (inputHandler.a_Input)
            {
                if (inputHandler.moveAmount > 0)
                {
                    moveDirection = cameraObject.forward * inputHandler.vertical;
                    moveDirection += cameraObject.right * inputHandler.horizontal;
                    animationHandler.PlayTargetAnimation("Jump", true);
                    moveDirection.y = 0;
                    Quaternion jumpRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = jumpRotation;
                    jumpForceApplied = true;
                }
            }
        }



        #endregion

        #region Jump
        public void HandleFalling(Vector3 moveDirection)
        {
            playerManager.isGrounded = false;
            RaycastHit hit;
            Vector3 origin = myTransform.position;
            origin.y += groundDetectionRayStartPoint;

            if (Physics.Raycast(origin, myTransform.forward, out hit, 0.4f))
            {
                moveDirection = Vector3.zero;
            }
            if (playerManager.isInAir)
            {

                rb.AddForce(-Vector3.up * fallSpeed);
                rb.AddForce(moveDirection * fallSpeed / 10f);
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * groundDirectionRayDistance;

            targetPosition = myTransform.position;

            Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededTobeginFall, Color.red, 0.1f, false);
            if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededTobeginFall, ignoreforGrounCheck))
            {
                normalVector = hit.normal;
                Vector3 tp = hit.point;
                playerManager.isGrounded = true;
                targetPosition.y = tp.y;



                if (playerManager.isInAir)
                {
                    if (InAirTimer > 0.5f)
                    {
                        animationHandler.PlayTargetAnimation("Land", true);
                    }
                    else
                    {
                        animationHandler.PlayTargetAnimation("Empty", false);
                        InAirTimer = 0;
                    }

                    playerManager.isInAir = false;

                }
            }
            else
            {
                if (playerManager.isGrounded)
                {
                    playerManager.isGrounded = false;
                }

                if (playerManager.isInAir == false)
                {
                    if (playerManager.isInteracting == false)
                    {
                        animationHandler.PlayTargetAnimation("Falling", true);
                    }

                    Vector3 vel = rb.velocity;
                    vel.Normalize();
                    rb.velocity = vel * (movementSpeed / 2);
                    playerManager.isInAir = true;
                }

            }
            if (playerManager.isInteracting || inputHandler.moveAmount > -1)
            {
                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                myTransform.position = targetPosition;
            }

        }

        /*public void DoJump()
        {
            moveDirection += Vector3.up * jumpHeight;
            rb.AddForce(moveDirection, ForceMode.Impulse);
        }*/
        #endregion

    }
}
