using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    /*#region Old Code
    private CharacterController controller;
    private Transform cameraTransform;
    private Rigidbody rb;
    private PlayerInput playerInput;
    private InputAction movementAction;
    private InputAction jumpAction;
    float gravityValue = -9.86f;

    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private Transform castPoint;

    [HideInInspector] public AnimationHandler animationHandler;
    [SerializeField] private float rotationSpeed = 5f;
    
 

    [Header("Movement")]
    [SerializeField] public float maxSpeed = 5f;
    [SerializeField] private float movementForce = 1.0f;
    private Vector3 playerVelocity = Vector3.zero;
  

    [Header("Jump")]
    [SerializeField] private float JumpHeight = 1.0f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 1.5f;



    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        movementAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        animationHandler.Initialize();
    }

    void FixedUpdate()
    {
        bool groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        //Movenment
        Vector2 input = movementAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        controller.Move(move * Time.deltaTime * maxSpeed);

        animationHandler.UpdateAnimatorValues(input.y, 0);

        if (move != Vector3.zero)
            gameObject.transform.forward = move;

     
        //Jump
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(JumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Rotate towards camera direction
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);
    }

    public Transform GetCastPoint
    {
        get { return castPoint; }
    }
    #endregion*/

    Transform cameraObject;
    InputHandler inputHandler;
    PlayerManager playerManager;
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
    [Space]
    [SerializeField]
    float fallSpeed = 45f;
    [SerializeField]
    float jumpHeight = 50;

    bool jumpForceApplied;
  
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        playerManager = GetComponent<PlayerManager>();
        cameraObject = Camera.main.transform;
        myTransform = transform;
        ignoreforGrounCheck = ~ignoreforGrounCheck;
        animationHandler.Initialize();

        playerManager.isGrounded = true;
    }

    private void Update()
    {
        if (jumpForceApplied)
        {
            StartCoroutine(JumpCo());
            rb.AddForce(moveDirection * movementSpeed + transform.up * jumpHeight);

        }
    }
    private IEnumerator JumpCo()
    {
        yield return new WaitForSeconds(0.35f);
        jumpForceApplied = false;
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

        animationHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

        if (animationHandler.canRotate)
        {
            HandleRotation();
        }
    }
    public void HandleRotation()
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
    public void HandleRollingandSprinting( )
    {
        if (animationHandler.anim.GetBool("isInteracting"))
            return;

        if(inputHandler.rollflag)
        {
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;

            if(inputHandler.moveAmount > 0)
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

        if (inputHandler.a_Input)
        {
            if(inputHandler.moveAmount > 0)
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
        if(playerManager.isInAir)
        {
            
                rb.AddForce(-Vector3.up * fallSpeed);
                rb.AddForce(moveDirection * fallSpeed / 10f);
        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        origin = origin + dir * groundDirectionRayDistance;

        targetPosition = myTransform.position;

        Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededTobeginFall, Color.red, 0.1f, false);
        if(Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededTobeginFall, ignoreforGrounCheck))
        {
            normalVector = hit.normal;
            Vector3 tp = hit.point;
            playerManager.isGrounded = true;
            targetPosition.y = tp.y;

            if(playerManager.isInAir)
            {
                if(InAirTimer > 0.5f)
                {
                   animationHandler.PlayTargetAnimation("Land", true);
                }
                else
                {
                    animationHandler.PlayTargetAnimation("Empty", false);
                }

                playerManager.isInAir = false;
                InAirTimer = 0;
            }
        }
        else
        {
            if(playerManager.isGrounded)
            {
                playerManager.isGrounded = false;
            }

            if(playerManager.isInAir == false)
            {
                if(!playerManager.isInteracting)
                {
                   animationHandler.PlayTargetAnimation("Falling", true);
                }

                Vector3 vel = rb.velocity;
                vel.Normalize();
                rb.velocity = vel * (movementSpeed / 2);
                playerManager.isInAir = true;
            }

           
                if(playerManager.isInteracting || inputHandler.moveAmount > 0)
                {
                    myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime/0.1f);
                }
                else
                {
                    myTransform.position = targetPosition;
                }
            }
    }

    /*public void DoJump()
    {
        moveDirection += Vector3.up * jumpHeight;
        rb.AddForce(moveDirection, ForceMode.Impulse);
    }*/
    #endregion

}
