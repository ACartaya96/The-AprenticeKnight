using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    AnimationHandler animationHandler;
    InputHandler inputHandler;
    PlayerManager playerManager;
    WeaponSlotManager weaponSlotManager;
    PlayerStats playerStats;
    PlayerInventory playerInventory;
    PlayerTargetDetection playerTarget;
    public string lastAttack;


    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        playerManager = GetComponent<PlayerManager>();
        playerStats = GetComponent<PlayerStats>();
        playerInventory = GetComponent<PlayerInventory>();
        playerTarget = GetComponent<PlayerTargetDetection>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        inputHandler = GetComponent<InputHandler>();

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

        switch(type)
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
        if(playerInventory.currentSpell != null)
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
