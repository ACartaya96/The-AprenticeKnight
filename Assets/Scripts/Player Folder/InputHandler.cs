using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    [Header("Face Buttons")]
    public bool b_Input;
    public bool a_Input;

    [Header("Trigger & Shoulders")]
    public bool rb_Input;
    public bool rt_Input;

    [Header("Sticks")]
    public bool right_Stick_Right;
    public bool right_Stick_Left;
    public bool rj_Input;

    public bool rollflag;
    public bool jumpflag;
    public bool comboflag;
    public bool lockedOnflag;
    public bool rbflag;
    public bool rtflag;




    PlayerInput playerInput;
    PlayerAttack playerAttack;
    PlayerInventory playerInventory;
    PlayerManager playerManager;
    PlayerTargetDetection playerTarget;
    PlayerController playerController;

    [HideInInspector]
    public InputAction moveAction;
    [HideInInspector]
    public InputAction lookAction;
    [HideInInspector]
    public InputAction jumpAction;
    [HideInInspector]
    public InputAction aimAction;
    [HideInInspector]
    public InputAction castAction;
    [HideInInspector]
    public InputAction rollAction;
    [HideInInspector]
    public InputAction lightAtkAction;
    [HideInInspector]
    public InputAction rLockOnAction;
    [HideInInspector]
    public InputAction lLockOnAction;


    Vector2 movementInput;
    Vector2 cameraInput;

    #region Instantiate Inputs
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAttack = GetComponentInChildren<PlayerAttack>();
        playerInventory = GetComponent<PlayerInventory>();
        playerManager = GetComponent<PlayerManager>();
        playerTarget = GetComponent<PlayerTargetDetection>();
        playerController = GetComponent<PlayerController>();

        moveAction = playerInput.actions["Movement"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
        castAction = playerInput.actions["Spell Cast"];
        rollAction = playerInput.actions["Roll"];
        aimAction = playerInput.actions["Aim"];
        lightAtkAction = playerInput.actions["Light Attack"];
        rLockOnAction = playerInput.actions["Lock On Target Right"];
        lLockOnAction = playerInput.actions["Lock On Target Left"];
    }
    private void OnEnable()
    {

        moveAction.performed += _ => movementInput = moveAction.ReadValue<Vector2>();
        lookAction.performed += _ => cameraInput = lookAction.ReadValue<Vector2>();
        rollAction.started += _ => b_Input = true;
        jumpAction.started += _ => a_Input = true;
        lightAtkAction.started += _ => rb_Input = true;
        castAction.started += _ => rt_Input = true;
        aimAction.performed += _ => rj_Input = true;
        lLockOnAction.performed += _ => right_Stick_Left = true;
        rLockOnAction.performed += _ => right_Stick_Right = true;
        
   

    }

    private void OnDisable()
    {
        moveAction.performed -= _ => movementInput = moveAction.ReadValue<Vector2>();
        lookAction.performed -= _ => cameraInput = lookAction.ReadValue<Vector2>();
        rollAction.started -= _ => b_Input = true;
        jumpAction.started -= _ => a_Input = true;
        lightAtkAction.started -= _ => rb_Input = true;
        castAction.started -= _ => rt_Input = true;
        aimAction.performed -= _ => rj_Input = true;
        lLockOnAction.performed -= _ => right_Stick_Left = true;
        rLockOnAction.performed -= _ => right_Stick_Right = true;

    }
    #endregion
    public void TickInput()
    {
        HandleMoveInput();
        HandleRollinput();
        HandleAttackInput();
        HandleLockOnInput();
    }
    #region HandleInputs
    private void HandleMoveInput()
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

   private void HandleRollinput()
    {
       
        if (b_Input)
            rollflag = true;
  
    }


    private void HandleAttackInput()
    {

        

        if (rb_Input)
        {
            if(lockedOnflag)
                playerController.HandleRotation();
            playerAttack.HandleRBAction();
        }
         
        if (rt_Input)
        {
            if (lockedOnflag)
                playerController.HandleRotation();
            playerAttack.HandleRTAction();
            
        }




    }

    private void HandleLockOnInput()
    {
        if(rj_Input && !lockedOnflag)
        {
            rj_Input = false;
            
            
            playerTarget.HandleLockOn();
            if(playerTarget.nearestLockOnTarget != null)
            {
                lockedOnflag = true;
                playerTarget.currentLockedOnTarget = playerTarget.nearestLockOnTarget;
            }
        }
        else if(rj_Input && lockedOnflag)
        {
            rj_Input = false;
            lockedOnflag = false;
            playerTarget.ClearLockOnTarget();
        }

        if(lockedOnflag && right_Stick_Left)
        {
            right_Stick_Left = false;
            playerTarget.HandleLockOn();
            if(playerTarget.leftLockOnTarget != null)
            {
                playerTarget.currentLockedOnTarget = playerTarget.leftLockOnTarget;
            }
        }


        if (lockedOnflag && right_Stick_Right)
        {
            right_Stick_Right = false;
            playerTarget.HandleLockOn();
            if (playerTarget.rightLockOnTarget != null)
            {
                playerTarget.currentLockedOnTarget = playerTarget.rightLockOnTarget;
                
            }
        }
    }
    #endregion

}
