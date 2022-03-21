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
    public bool isInAir;
    public bool isGrounded;

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
        
        Debug.Log("PlayerManager: " + inputHandler.rollflag.ToString());
    }

    private void FixedUpdate()
    {


      
        inputHandler.TickInput();
        playerController.HandleMovement();
        playerController.HandleRollingandSprinting();
        playerController.HandleFalling(playerController.moveDirection);
        playerController.HandleJump();
    }

    private void LateUpdate()
    {
        inputHandler.rollflag = false;
        if(isInAir)
        {
            playerController.InAirTimer = playerController.InAirTimer + Time.deltaTime;
        }
        if(!isGrounded)
        {
            inputHandler.jumpflag = false;
        }
    }
}
