using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputHandler inputHandler;
    PlayerController playerController;
    Animator anim;

    [Header("Player Flags")]
    public bool isInteracting;
    [Space]
    public bool isInAir;
    public bool isGrounded;
    public bool canDoCombo;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        playerController = GetComponent<PlayerController>();
        anim = GetComponentInChildren<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
       
        isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");
        anim.SetBool("isInAir", isInAir);

        inputHandler.TickInput();
        playerController.HandleMovement();
        playerController.HandleRollingandSprinting();
        playerController.HandleFalling(playerController.moveDirection);
        playerController.HandleJumping();
    }

    private void FixedUpdate()
    {
              
        
    }

    private void LateUpdate()
    {
        inputHandler.rollflag = false;
        inputHandler.b_Input = false;
        inputHandler.a_Input = false;
        inputHandler.rb_Input = false;
        inputHandler.rt_Input = false;
        
        if(isInAir)
        {
            playerController.InAirTimer = playerController.InAirTimer + Time.deltaTime;
        }

    }
}
