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
    public bool rollflag;
    public bool jumpflag;
    

    PlayerInput playerInput;
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


    Vector2 movementInput;
    Vector2 cameraInput;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Movement"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
        aimAction = playerInput.actions["Aim"];
        castAction = playerInput.actions["Spell Cast"];
        rollAction = playerInput.actions["Roll"];
    }
    public void OnEnable()
    {

        moveAction.performed += _ => movementInput = moveAction.ReadValue<Vector2>();
        lookAction.performed += _ => cameraInput = lookAction.ReadValue<Vector2>();
        jumpAction.performed += _ => a_Input = true;
        jumpAction.canceled += _ => a_Input = false;

        //rollAction.performed += _ => HandleRollinput(); 

    }

    private void OnDisable()
    {
        moveAction.performed -= _ => movementInput = moveAction.ReadValue<Vector2>();
        lookAction.performed -= _ => cameraInput = lookAction.ReadValue<Vector2>();
        jumpAction.performed -= _ => a_Input = true;
        jumpAction.performed -= _ => a_Input = false;
        //rollAction.performed -= _ => HandleRollinput();
    }

    public void TickInput()
    {
        MoveInput();
        HandleRollinput();
        HandleJumpInput();
    }

    private void MoveInput()
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
   
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    public void HandleRollinput()
    {
        if (rollAction.IsPressed())
            b_Input = true;
        else
            b_Input = false;
  

        if(b_Input)
            rollflag = true;      
    }

    public void HandleJumpInput()
    { 
        if (a_Input)
            jumpflag = true;

    }

    private void JumpAction_performed(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }
}
