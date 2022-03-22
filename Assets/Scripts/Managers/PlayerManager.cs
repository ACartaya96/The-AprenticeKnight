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
        playerController.HandleJumping();
        playerController.HandleRollingandSprinting();

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
        inputHandler.rj_Input = false;
        
        if(isInAir)
        {
            playerController.InAirTimer = playerController.InAirTimer + Time.deltaTime;
        }

    }
}
