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

    public bool b_Input;
    public bool a_Input;
    public bool rb_Input;
    public bool rt_Input;
    public bool rj_Input;
    public bool rollflag;
    public bool jumpflag;
    public bool comboflag;
 


    PlayerInput playerInput;
    PlayerAttack playerAttack;
    PlayerInventory playerInventory;
    PlayerManager playerManager;

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


    Vector2 movementInput;
    Vector2 cameraInput;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAttack = GetComponentInChildren<PlayerAttack>();
        playerInventory = GetComponent<PlayerInventory>();
        playerManager = GetComponent<PlayerManager>();

        moveAction = playerInput.actions["Movement"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
        castAction = playerInput.actions["Spell Cast"];
        rollAction = playerInput.actions["Roll"];
        aimAction = playerInput.actions["Aim"];
        lightAtkAction = playerInput.actions["Light Attack"];
    }
    private void OnEnable()
    {

        moveAction.performed += _ => movementInput = moveAction.ReadValue<Vector2>();
        lookAction.performed += _ => cameraInput = lookAction.ReadValue<Vector2>();
        rollAction.started += _ => b_Input = true;
        jumpAction.started += _ => a_Input = true;
        lightAtkAction.started += _ => rb_Input = true;
        castAction.started += _ => rt_Input = true;
        aimAction.started += _ => rj_Input = true;
        
        //lightAtkAction.canceled += _ => rb_Input = false;
        //castAction.performed += _ => rt_Input = true;
        //castAction.canceled += _ => rt_Input = false;

        //rollAction.performed += _ => HandleRollinput(); 

    }

    private void OnDisable()
    {
        moveAction.performed -= _ => movementInput = moveAction.ReadValue<Vector2>();
        lookAction.performed -= _ => cameraInput = lookAction.ReadValue<Vector2>();
        rollAction.started -= _ => b_Input = true;
        jumpAction.started -= _ => a_Input = true;
        lightAtkAction.started -= _ => rb_Input = true;
        castAction.started -= _ => rt_Input = true;
        aimAction.started -= _ => rj_Input = true;
    }

    public void TickInput()
    {
        MoveInput();
        HandleRollinput();
        HandleAttackInput();
    }

    private void MoveInput()
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
            playerAttack.HandleRBAction();
        }
         
        if (rt_Input)
        {
            if (playerManager.canDoCombo)
            {
                comboflag = true;
                playerAttack.HeavyHandleWeaponCombo(playerInventory.rightWeapon);
                comboflag = false;
            }
            else
            {
                playerAttack.HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }




    }

}
