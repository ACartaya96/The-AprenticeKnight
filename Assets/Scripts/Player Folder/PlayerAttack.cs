using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace TAK
{
    public class PlayerAttack : MonoBehaviour
    {

        AnimationHandler animationHandler;
        InputHandler inputHandler;
        PlayerManager playerManager;
        WeaponSlotManager weaponSlotManager;
        PlayerStats playerStats;
        PlayerInventory playerInventory;
        PlayerEquipmentManager playerEquipment;
        PlayerTargetDetection playerTarget;
        PlayerAudioManager playerAudioManager;
        public string lastAttack;


        private void Awake()
        {
            animationHandler = GetComponent<AnimationHandler>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            playerEquipment = GetComponent<PlayerEquipmentManager>();
            playerTarget = GetComponentInParent<PlayerTargetDetection>();
            playerAudioManager = GetComponent<PlayerAudioManager>();
            weaponSlotManager = GetComponentInParent<WeaponSlotManager>();
            inputHandler = GetComponentInParent<InputHandler>();

        }

        public void LightHandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboflag)
            {
                animationHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.Right_Attack_1)
                {
                    animationHandler.PlayTargetAnimation(weapon.Right_Attack_2, true);
                }
                else if(lastAttack == weapon.Left_Attack_1)
                {
                    animationHandler.PlayTargetAnimation(weapon.Left_Attack_2, true);
                }
            }
        }

        public void HeavyHandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboflag)
            {
                animationHandler.anim.SetBool("canDoCombo", false);
                if (lastAttack == weapon.OH_Heavy_Attack_1)
                {
                    animationHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_2, true);
                }
            }
        }
        public void HandleRightAttack(WeaponItem weapon)
        {
           animationHandler.PlayTargetAnimation(weapon.Right_Attack_1, true);
                lastAttack = weapon.Right_Attack_1;
            

        }

        public void HandleLeftAttack(WeaponItem weapon)
        {
            
                animationHandler.PlayTargetAnimation(weapon.Left_Attack_1, true);
                lastAttack = weapon.Left_Attack_1;

        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            animationHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
            lastAttack = weapon.OH_Heavy_Attack_1;
        }

        public void HandleSpecialAttack(WeaponItem weapon)
        {
            animationHandler.PlayTargetAnimation(weapon.LT_Special_Attack, true);
            //lastAttack = weapon.OH_Heavy_Attack_1;
        }

        #region Input Actions
        public void HandleRBAction()
        {
            WeaponType type = playerInventory.rightWeapon.weaponType;

            switch (type)
            {
                case WeaponType.Weapon:
                    PerformRBMeleeAction();
                    break;

                case WeaponType.Staff:
                    PerformRBSpellAction();
                    break;

                case WeaponType.Shield:
                    PerformRBBlockAction();
                    break;

            }
        }


        #region RB Actions
        private void PerformRBMeleeAction()
        {
            if (playerManager.canDoCombo)
            {
                inputHandler.comboflag = true;
                LightHandleWeaponCombo(playerInventory.rightWeapon);
                inputHandler.comboflag = false;
            }
            else
            {
                if (animationHandler.anim.GetBool("isInteracting"))
                    return;

                HandleRightAttack(playerInventory.rightWeapon);
            }
            inputHandler.rb_Input = false;
        }

        private void PerformRBSpellAction()
        {
            if (playerInventory.currentSpell != null)
            {
                if (animationHandler.anim.GetBool("isInteracting"))
                    return;

                if (playerStats.currentMana < playerInventory.currentSpell.cost)
                    animationHandler.PlayTargetAnimation("Out Of Mana", true);
                else
                    playerInventory.currentSpell.AttemptToCastSpell(animationHandler, playerStats, weaponSlotManager, playerAudioManager);
            }
            inputHandler.rb_Input = false;
        }
        private void SuccessfullyCastSpell()
        {
            playerInventory.currentSpell.SuccessfullyCastSpell(animationHandler, playerStats, weaponSlotManager, playerManager, playerTarget, playerAudioManager);
            animationHandler.anim.SetBool("isFiringSpell", true);

        }
        private void PerformRBBlockAction()
        {
            if (playerManager.isInteracting)
                return;

            if (playerManager.isBlocking)
                return;

            animationHandler.PlayTargetAnimation("Block Start 2", false);
            playerEquipment.OpenBlockingCollider();
            playerManager.isBlocking = true;
        }
        #endregion
        #region LB Actions
        public void HandleLBAction()
        {

            WeaponType type = playerInventory.leftWeapon.weaponType;

            switch (type)
            {
                case WeaponType.Weapon:
                    PerformLBMeleeAction();
                    break;

                case WeaponType.Staff:
                    PerformLBSpellAction();
                    break;

                case WeaponType.Shield:
                    PerformLBBlockAction();
                    break;

            }
        }

        private void PerformLBMeleeAction()
        {
            if (playerManager.canDoCombo)
            {
                inputHandler.comboflag = true;
                LightHandleWeaponCombo(playerInventory.leftWeapon);
                inputHandler.comboflag = false;
            }
            else
            {
                if (animationHandler.anim.GetBool("isInteracting"))
                    return;

                HandleLeftAttack(playerInventory.leftWeapon);

                inputHandler.lb_Input = false;
            }
        }

        private void PerformLBSpellAction()
        {
            if (playerInventory.currentSpell != null)
            {
                if (animationHandler.anim.GetBool("isInteracting"))
                    return;

                if (playerStats.currentMana < playerInventory.currentSpell.cost)
                    animationHandler.PlayTargetAnimation("Out Of Mana", true);
                else
                    playerInventory.currentSpell.AttemptToCastSpell(animationHandler, playerStats, weaponSlotManager, playerAudioManager);
            }
        }
        private void PerformLBBlockAction()
        {
            if (playerManager.isInteracting)
                return;

            if (playerManager.isBlocking)
                return;

            animationHandler.PlayTargetAnimation("Block Start", false);
            playerEquipment.OpenBlockingCollider();
            playerManager.isBlocking = true;
        }
        #endregion

        #endregion

        public void HandleRTAction()
        {
            if (playerManager.canDoCombo)
            {

                inputHandler.comboflag = true;
                HeavyHandleWeaponCombo(playerInventory.rightWeapon);
                inputHandler.comboflag = false;
            }
            else
            {
                if (animationHandler.anim.GetBool("isInteracting"))
                    return;
                HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }

        public void HandleLTAction()
        {
            /*if (playerManager.canDoCombo)
            {

                inputHandler.comboflag = true;
                HeavyHandleWeaponCombo(playerInventory.rightWeapon);
                inputHandler.comboflag = false;
            }*/
            
            
                if (animationHandler.anim.GetBool("isInteracting"))
                    return;
                HandleSpecialAttack(playerInventory.leftWeapon);
            
        }
    }
}
