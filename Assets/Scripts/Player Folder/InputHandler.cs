using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace TAK
{
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
        public bool lb_Input;
        public bool lt_Input;

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
        PlayerEquipmentManager playerEquipment;

        [HideInInspector]
        public InputAction moveAction;
        [HideInInspector]
        public InputAction lookAction;
        [HideInInspector]
        public InputAction jumpAction;
        [HideInInspector]
        public InputAction aimAction;
        [HideInInspector]
        public InputAction rtAction;
        [HideInInspector]
        public InputAction rollAction;
        [HideInInspector]
        public InputAction rbAction;
        [HideInInspector]
        public InputAction rLockOnAction;
        [HideInInspector]
        public InputAction lLockOnAction;
        [HideInInspector]
        public InputAction lbAction;
        [HideInInspector]
        public InputAction ltAction;



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
            playerEquipment = GetComponentInChildren<PlayerEquipmentManager>();

            moveAction = playerInput.actions["Movement"];
            lookAction = playerInput.actions["Look"];
            jumpAction = playerInput.actions["Jump"];
            rtAction = playerInput.actions["RT"];
            ltAction = playerInput.actions["LT"];
            rollAction = playerInput.actions["Roll"];
            aimAction = playerInput.actions["Aim"];
            rbAction = playerInput.actions["RB"];
            lbAction = playerInput.actions["LB"];
            rLockOnAction = playerInput.actions["Lock On Target Right"];
            lLockOnAction = playerInput.actions["Lock On Target Left"];
        }
        private void OnEnable()
        {

            moveAction.performed += _ => movementInput = moveAction.ReadValue<Vector2>();
            lookAction.performed += _ => cameraInput = lookAction.ReadValue<Vector2>();
            rollAction.started += _ => b_Input = true;
            jumpAction.started += _ => a_Input = true;
            rbAction.performed += _ => rb_Input = true;
            lbAction.performed += _ => lb_Input = true;
            lbAction.canceled += _ => lb_Input = false;
            rtAction.performed += _ => rt_Input = true;
            ltAction.performed += _ => lt_Input = true;
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
            rbAction.performed -= _ => rb_Input = true;
            lbAction.performed -= _ => lb_Input = true;
            lbAction.canceled -= _ => lb_Input = false;
            rtAction.performed -= _ => rt_Input = true;
            ltAction.performed -= _ => lt_Input = true;
            aimAction.performed -= _ => rj_Input = true;
            lLockOnAction.performed -= _ => right_Stick_Left = true;
            rLockOnAction.performed -= _ => right_Stick_Right = true;

        }
        #endregion
        public void TickInput()
        {
            HandleMoveInput();
            HandleRollinput();
            HandleCombatInput();
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


        private void HandleCombatInput()
        {



            if (rb_Input)
            {
                if (lockedOnflag)
                {
                    playerController.HandleRotation();
                }

                playerAttack.HandleRBAction();
            }
            if (rt_Input)
            {
                if (lockedOnflag)
                {
                    playerController.HandleRotation();
                }
                playerAttack.HandleRTAction();

            }
            if (lb_Input)
            {
               
                playerAttack.HandleLBAction();
            }
            else
            {
                playerManager.isBlocking = false;
                playerEquipment.CloseBlockingCollider();  
            }




        }

        private void HandleLockOnInput()
        {
            if (rj_Input && !lockedOnflag)
            {
                rj_Input = false;


                playerTarget.HandleLockOn();
                if (playerTarget.nearestLockOnTarget != null)
                {
                    lockedOnflag = true;
                    playerTarget.currentLockedOnTarget = playerTarget.nearestLockOnTarget;
                }
            }
            else if (rj_Input && lockedOnflag)
            {
                rj_Input = false;
                lockedOnflag = false;
                playerTarget.ClearLockOnTarget();
            }

            if (lockedOnflag && right_Stick_Left)
            {
                right_Stick_Left = false;
                playerTarget.HandleLockOn();
                if (playerTarget.leftLockOnTarget != null)
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
}
