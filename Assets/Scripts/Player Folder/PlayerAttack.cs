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
        public string lastAttack;


        private void Awake()
        {
            animationHandler = GetComponent<AnimationHandler>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            playerEquipment = GetComponent<PlayerEquipmentManager>();
            playerTarget = GetComponentInParent<PlayerTargetDetection>();
            weaponSlotManager = GetComponentInParent<WeaponSlotManager>();
            inputHandler = GetComponentInParent<InputHandler>();

        }

        public void LightHandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboflag)
            {
                animationHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.OH_Light_Attack_1)
                {
                    animationHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
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
        public void HandleLightAttack(WeaponItem weapon)
        {
            animationHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
            lastAttack = weapon.OH_Light_Attack_1;

        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            animationHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
            lastAttack = weapon.OH_Heavy_Attack_1;
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

                HandleLightAttack(playerInventory.rightWeapon);
            }
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
                    playerInventory.currentSpell.AttemptToCastSpell(animationHandler, playerStats, weaponSlotManager);
            }
        }
        private void SuccessfullyCastSpell()
        {
            playerInventory.currentSpell.SuccessfullyCastSpell(animationHandler, playerStats, weaponSlotManager, playerManager, playerTarget);
            animationHandler.anim.SetBool("isFiringSpell", true);

        }
        private void PerformRBBlockAction()
        {

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

                HandleLightAttack(playerInventory.leftWeapon);
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
                    playerInventory.currentSpell.AttemptToCastSpell(animationHandler, playerStats, weaponSlotManager);
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

    }
}
