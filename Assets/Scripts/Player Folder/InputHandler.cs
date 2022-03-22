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
    public bool rollflag;
    public bool jumpflag;


    PlayerInput playerInput;
    PlayerAttack playerAttack;
    PlayerInventory playerInventory;

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

        moveAction = playerInput.actions["Movement"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
        castAction = playerInput.actions["Spell Cast"];
        rollAction = playerInput.actions["Roll"];
        lightAtkAction = playerInput.actions["Light Attack"];
    }
    private void OnEnable()
    {

        moveAction.performed += _ => movementInput = moveAction.ReadValue<Vector2>();
        lookAction.performed += _ => cameraInput = lookAction.ReadValue<Vector2>();
        //jumpAction.performed += _ => a_Input = true;
        //jumpAction.canceled += _ => a_Input = false;
        //lightAtkAction.performed += _ => rb_Input = true;
        //lightAtkAction.canceled += _ => rb_Input = false;
        //castAction.performed += _ => rt_Input = true;
        //castAction.canceled += _ => rt_Input = false;

        //rollAction.performed += _ => HandleRollinput(); 

    }

    private void OnDisable()
    {
        moveAction.performed -= _ => movementInput = moveAction.ReadValue<Vector2>();
        lookAction.performed -= _ => cameraInput = lookAction.ReadValue<Vector2>();
        //jumpAction.performed -= _ => a_Input = true;
        //jumpAction.performed -= _ => a_Input = false;
        //lightAtkAction.performed -= _ => rb_Input = true;
       // lightAtkAction.canceled -= _ => rb_Input = false;
        //castAction.performed -= _ => rt_Input = true;
        //castAction.canceled -= _ => rt_Input = false;
        //rollAction.performed -= _ => HandleRollinput();
    }

    public void TickInput()
    {
        MoveInput();
        HandleRollinput();
        HandleJumpInput();
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
        if (rollAction.IsPressed())
            b_Input = true;
        else
            b_Input = false;


        if (b_Input)
            rollflag = true;
    }

    private void HandleJumpInput()
    {
        if (jumpAction.IsPressed())
            a_Input = true;

    }

    private void HandleAttackInput()
    {
        if (lightAtkAction.IsPressed())
            rb_Input = true;
        
        if (castAction.IsPressed())
            rt_Input = true;

        if (rb_Input)
        {
            Debug.Log("Light Attack Pressed");
            playerAttack.HandleLightAttack(playerInventory.rightWeapon);

        }
         
        if (rt_Input)
        {
            Debug.Log("Heavy Attack Pressed");
            playerAttack.HandleHeavyAttack(playerInventory.rightWeapon);
        }




    }

}
