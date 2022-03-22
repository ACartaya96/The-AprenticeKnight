using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    AnimationHandler animationHandler;
    InputHandler inputHandler;
    PlayerManager playerManager;
    PlayerStats playerStats;
    PlayerInventory playerInventory;
    public string lastAttack;


    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        playerManager = GetComponentInParent<PlayerManager>();
        playerStats = GetComponentInParent<PlayerStats>();
        playerInventory = GetComponentInParent<PlayerInventory>();
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
        if (playerInventory.rightWeapon.weaponType is WeaponItem.WeaponType.Melee)
        {
            PerformRBMeleeAction();
        }
        else if (playerInventory.rightWeapon.weaponType is WeaponItem.WeaponType.Spellcasting)
        {
            PerformRBSpellAction();
        }
    }

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
            HandleLightAttack(playerInventory.rightWeapon);
        }
    }

    private void PerformRBSpellAction()
    {
        if(playerInventory.currentSpell != null)
        {
            if (playerStats.currentMana < playerInventory.currentSpell.cost)
                animationHandler.PlayTargetAnimation("Out Of Mana", true);
            else
                playerInventory.currentSpell.AttemptToCastSpell(animationHandler, playerStats);
        }
    }

    private void SuccessfullyCastSpell()
    {
        playerInventory.currentSpell.SuccessfullyCastSpell(animationHandler, playerStats);
    }
    #endregion
}
