using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    InputHandler inputHandler;
    PlayerController playerController;
    Animator anim;
    PlayerTargetDetection playerTarget;

    [Header("References")]
    public Transform cameraTarget;
    [HideInInspector]
    public Transform currentLockedOnTarget;

    [Header("Player Flags")]
    public bool isInteracting;
    [Space]
    public bool isInAir;
    public bool isGrounded;
    public bool canDoCombo;
    
    public bool isInvincible;
 

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        playerController = GetComponent<PlayerController>();
        playerTarget = GetComponent<PlayerTargetDetection>();
        anim = GetComponentInChildren<Animator>();

    
    }


    // Update is called once per frame
    void Update()
    {
       
        isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");
        isFiringSpell = anim.GetBool("isFiringSpell");
        anim.SetBool("isInAir", isInAir);
        inputHandler.TickInput();

        playerController.HandleRollingandSprinting();
        playerController.HandleJumping();

    }

    private void FixedUpdate()
    {
        playerController.HandleMovement();
        playerController.HandleFalling(playerController.moveDirection);
        
    }

    private void LateUpdate()
    {
        inputHandler.rollflag = false;
        inputHandler.b_Input = false;
        inputHandler.a_Input = false;
        inputHandler.rb_Input = false;
        inputHandler.rt_Input = false;
        inputHandler.rbflag = false;
        inputHandler.rtflag = false;
        
        if(currentLockedOnTarget == null)
        {
            inputHandler.lockedOnflag = false;
        }

        if(isInAir)
        {
            playerController.InAirTimer = playerController.InAirTimer + Time.deltaTime;
        }

    }
}
