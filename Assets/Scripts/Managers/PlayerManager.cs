using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class PlayerManager : CharacterManager
    {
        InputHandler inputHandler;
        PlayerController playerController;
        Animator anim;
        AnimationHandler animationHandler;
        PlayerTargetDetection playerTarget;
        PlayerEffectManager playerEffectManager;

        [Header("References")]
        public Transform cameraTarget;


        [Header("Player Flags")]
       
        
        public bool canDoCombo;




        // Start is called before the first frame update
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            playerController = GetComponent<PlayerController>();
            playerTarget = GetComponent<PlayerTargetDetection>();
            anim = GetComponentInChildren<Animator>();
            playerEffectManager = GetComponentInChildren<PlayerEffectManager>();
            animationHandler = GetComponentInChildren<AnimationHandler>();


        }


        // Update is called once per frame
        void Update()
        {

            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            isFiringSpell = anim.GetBool("isFiringSpell");
            anim.SetBool("isLockedOn", inputHandler.lockedOnflag);
            anim.SetBool("isInAir", isInAir);
            anim.SetBool("isBlocking", isBlocking);
            
            animationHandler.canRotate = anim.GetBool("canRotate");
            inputHandler.TickInput();

            playerController.HandleRollingandSprinting();
            playerController.HandleJumping();

            if (isInteracting && inputHandler.lockedOnflag)
            {
                SwitchVCam.instance.vcam.m_RecenterToTargetHeading.m_enabled = false;
            }
            else
            {
                SwitchVCam.instance.vcam.m_RecenterToTargetHeading.m_enabled = true;

            }

        }

        private void FixedUpdate()
        {
            playerController.HandleMovement();
            playerController.HandleRotation();
            playerController.HandleFalling(playerController.moveDirection);
            playerEffectManager.HandleAllBuildUpEffects();

            //Debug.Log(inputHandler.lt_Input.ToString());

        }

        private void LateUpdate()
        {



            inputHandler.rollflag = false;
            inputHandler.b_Input = false;
            inputHandler.a_Input = false;
            //inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            //inputHandler.rbflag = false;
            //inputHandler.rtflag = false;
            //inputHandler.lt_Input = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.d_Pad_Left = false;

            if (playerTarget.currentLockedOnTarget == null)
            {
                inputHandler.lockedOnflag = false;
            }

            if (isInAir)
            {
                playerController.InAirTimer = playerController.InAirTimer + Time.deltaTime;
            }





        }

    }
}
