using System;
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
        Camera cam;

        Rigidbody spellRb;
        public string lastAttack;


        private void Awake()
        {
            cam = Camera.main;
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
                else if (lastAttack == weapon.Left_Attack_1)
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

                if (playerStats.currentMana < playerInventory.currentSpell.spellManaCost)
                    animationHandler.PlayTargetAnimation("Out Of Mana", true);
                else
                    //playerInventory.currentSpell.AttemptToCastSpell(animationHandler, playerStats, weaponSlotManager, playerAudioManager);
                    AttemptToCastSpell(playerInventory.currentSpell);
            }
            inputHandler.rb_Input = false;
        }
        //private void SuccessfullyCastSpell()
        //{
        //    playerInventory.currentSpell.SuccessfullyCastSpell(animationHandler, playerStats, weaponSlotManager, playerManager, playerTarget, playerAudioManager);
        //    animationHandler.anim.SetBool("isFiringSpell", true);

        //}
        private void PerformRBBlockAction()
        {
            if (playerManager.isInteracting)
                return;

            if (playerManager.isBlocking)
                return;
            playerManager.isBlocking = true;
            animationHandler.PlayTargetAnimation("Block Start 2", false);
            playerEquipment.OpenBlockingCollider();

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

                if (playerStats.currentMana < playerInventory.currentSpell.spellManaCost)
                    animationHandler.PlayTargetAnimation("Out Of Mana", true);
                else
                    AttemptToCastSpell(playerInventory.currentSpell);
                    
            }
        }
        private void PerformLBBlockAction()
        {
            if (playerManager.isInteracting)
                return;

            if (playerManager.isBlocking)
                return;

            playerManager.isBlocking = true;
            animationHandler.PlayTargetAnimation("Block Start", false);
            playerEquipment.OpenBlockingCollider();

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

            WeaponType type = playerInventory.leftWeapon.weaponType;

            switch (type)
            {
                case WeaponType.Weapon:
                    PerformLTMeleeAction();
                    break;

                case WeaponType.Staff:
                    PerformLTSpellAction();
                    break;

                case WeaponType.Shield:
                    PerformLTBlockAction();
                    break;

            }



        }

        private void PerformLTBlockAction()
        {
            if (playerManager.isInteracting)
                return;

            if (playerManager.isBlocking)
                return;

            playerManager.isBlocking = true;
            animationHandler.PlayTargetAnimation("Block Start", false);
            playerEquipment.OpenBlockingCollider();

        }

        private void PerformLTSpellAction()
        {
            if (animationHandler.anim.GetBool("isInteracting"))
                return;
            HandleHeavyAttack(playerInventory.leftWeapon);
        }

        private void PerformLTMeleeAction()
        {
            if (animationHandler.anim.GetBool("isInteracting"))
                return;
            HandleSpecialAttack(playerInventory.leftWeapon);
        }

        #region New Spell System Set Up

        void AttemptToCastSpell(Spell spell)
        {
            GameObject istantiateWarmUpSpellFX = Instantiate(spell.spellWarmUpPrefab, weaponSlotManager.rightHandSlot.transform);
            //istantiateWarmUpSpellFX.gameObject.transform.localScale = new Vector3(100, 100, 100);
            animationHandler.PlayTargetAnimation(spell.startSpellAnimation, true);
            playerAudioManager.PlayTargetSoundEffect(spell.startUpSFX);
        }
        public void SuccessfullyCastSpell()
        {

            if (playerInventory.currentSpell.spellCastPrefab == null)
            {
                Debug.LogWarning("Spell prefab is null.Assign a spell prefab.");
                return;

            }

            GameObject spellObject = null;
            animationHandler.anim.SetBool("isFiringSpell", true);

            //We will find what type of spell we are using
            //
            //********************************SINGLE*********************************************
            if (playerInventory.currentSpell.spellType == Spell.SpellType.Single)
            {
                //Instantiated object will move straight forward.
                if (playerInventory.currentSpell.spellDirection == Spell.SpellDirection.Directional)
                {
                    spellObject = (GameObject)Instantiate(playerInventory.currentSpell.spellCastPrefab, weaponSlotManager.rightHandSlot.transform.position, weaponSlotManager.rightHandSlot.transform.rotation);
                    spellRb = spellObject.GetComponent<Rigidbody>();
                    
                    spellObject.name = playerInventory.currentSpell.name;
                    spellObject.GetComponent<SpellObjectConfiguration>().spell = playerInventory.currentSpell;
                    if (playerTarget.currentLockedOnTarget != null)
                    {
                        Vector3 dir = playerTarget.currentLockedOnTarget.position - spellObject.transform.position;

                        dir.Normalize();

                        Quaternion tr = Quaternion.LookRotation(dir);
                        Quaternion targetRotation = Quaternion.Slerp(spellObject.transform.rotation, tr, playerInventory.currentSpell.projectileForwardVelocity * Time.deltaTime);
                        spellObject.transform.rotation = targetRotation;
                        spellObject.transform.position = Vector3.MoveTowards(spellObject.transform.position, playerTarget.currentLockedOnTarget.transform.position, playerInventory.currentSpell.projectileForwardVelocity * Time.deltaTime/5f);
                  

                    }
                    else
                    {
                     

                        spellObject.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, playerStats.transform.eulerAngles.y, 0);

                    }

                     spellRb.AddForce(spellObject.transform.forward * playerInventory.currentSpell.projectileForwardVelocity);
                     spellRb.AddForce(spellObject.transform.up * playerInventory.currentSpell.projectileUpwardVelocity);
                     spellObject.transform.parent = null;

                }

                //Instantiated object will follow target.
                if (playerInventory.currentSpell.spellDirection == Spell.SpellDirection.Follow)
                {
                    spellObject = (GameObject)Instantiate(playerInventory.currentSpell.spellCastPrefab, weaponSlotManager.rightHandSlot.transform.position, weaponSlotManager.rightHandSlot.transform.rotation);
                    spellObject.name = playerInventory.currentSpell.itemName;
                 
                    if (playerTarget.currentLockedOnTarget != null)
                    { 
                        spellObject.GetComponent<SpellObjectConfiguration>().myTarget = playerTarget.currentLockedOnTarget;
                        transform.TransformDirection(Vector3.forward);
                        transform.Translate(new Vector3(0, 0, playerInventory.currentSpell.projectileForwardVelocity * Time.deltaTime));
                        transform.rotation = Quaternion.Slerp(transform.rotation,
                                                                Quaternion.LookRotation(playerTarget.currentLockedOnTarget.position - transform.position),
                                                                5 * Time.deltaTime);
                    }
                   
                }

                //Instantiating to target's position.
                if (playerInventory.currentSpell.spellDirection == Spell.SpellDirection.Point)
                {
                    spellObject = (GameObject)Instantiate(playerInventory.currentSpell.spellCastPrefab, transform.position, transform.rotation);
                   
                    spellObject.name = playerInventory.currentSpell.itemName;
                    if (playerTarget.currentLockedOnTarget != null)
                    {
                        spellObject.GetComponent<SpellObjectConfiguration>().myTarget = playerTarget.currentLockedOnTarget;
                    }
                }


            }

            //********************************AOE*********************************************
            else if (playerInventory.currentSpell.spellType == Spell.SpellType.Aoe)
            {
                if (playerInventory.currentSpell.spellPosition == Spell.SpellPosition.TargetTransform)
                    spellObject = (GameObject)Instantiate(playerInventory.currentSpell.spellCastPrefab, playerTarget.currentLockedOnTarget.position, Quaternion.identity);
                else
                    spellObject = (GameObject)Instantiate(playerInventory.currentSpell.spellCastPrefab, transform.position, Quaternion.identity);

                spellObject.name = playerInventory.currentSpell.itemName;


            }

            //********************************BUFF*********************************************
            else
            {
                //Spell type is a buff.And we are checking what type of buff spell is used.
                if (playerInventory.currentSpell.buffType == Spell.BuffType.Heal)
                {
                    spellObject = (GameObject)Instantiate(playerInventory.currentSpell.spellCastPrefab, transform.position, Quaternion.identity);
                    spellObject.name = playerInventory.currentSpell.itemName;
                    playerStats.HealPlayer(UnityEngine.Random.Range(playerInventory.currentSpell.minBuffAmount, playerInventory.currentSpell.maxBuffAmount));
                  

                }
                else if (playerInventory.currentSpell.buffType == Spell.BuffType.MagicalDefense)
                {
                    spellObject = (GameObject)Instantiate(playerInventory.currentSpell.spellCastPrefab,transform.position, Quaternion.identity);
                    spellObject.name = playerInventory.currentSpell.itemName;
                    //magicalDefense += (Random.Range(spell.minBuffAmount,spell.maxBuffAmount));	

                }
                else
                {
                    //Physical Defense
                    spellObject = (GameObject)Instantiate(playerInventory.currentSpell.spellCastPrefab, transform.position, Quaternion.identity);
                    spellObject.name = playerInventory.currentSpell.itemName;
                    //physicalDefense += (Random.Range(spell.minBuffAmount,spell.maxBuffAmount));	
                }

              
            }
            playerStats.UseMana(playerInventory.currentSpell.spellManaCost);
        }

    }
    #endregion
}

