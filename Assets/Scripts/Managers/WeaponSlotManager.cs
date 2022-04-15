using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class WeaponSlotManager : MonoBehaviour
    {
        PlayerInventory playerInventory;

        public WeaponHolderSlot leftHandSlot;
        public WeaponHolderSlot rightHandSlot;

        DamageCollider leftDamageCollider;
        DamageCollider rightDamageCollider;

        QuickSlotsUI quickSlots;

        private void Awake()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            quickSlots = FindObjectOfType<QuickSlotsUI>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        public void LoadBothWeaponOnSlots()
        {
            LoadWeaponOnSlot(playerInventory.rightWeapon, false);
            LoadWeaponOnSlot(playerInventory.leftWeapon, true);
        }
        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                if(weaponItem.weaponType != WeaponType.Shield)
                    LoadLeftWeaponDamageCollider();
                quickSlots.UpdateWeaponQuickSlotsUI(true, weaponItem);
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                if (weaponItem.weaponType != WeaponType.Shield)
                    LoadRightWeaponDamageCollider();
                quickSlots.UpdateWeaponQuickSlotsUI(false, weaponItem);
            }
        }
        #region Weapon's Damage Collider
        private void LoadLeftWeaponDamageCollider()
        {
            leftDamageCollider = leftHandSlot.currentWeapon.GetComponentInChildren<DamageCollider>(); 
            leftDamageCollider.characterManager = GetComponentInParent<PlayerManager>();
        }

        private void LoadRightWeaponDamageCollider()
        {
            rightDamageCollider = rightHandSlot.currentWeapon.GetComponentInChildren<DamageCollider>();
            rightDamageCollider.characterManager = GetComponentInParent<PlayerManager>();
        }

        public void OpenRightDamageCollider()
        {
            rightDamageCollider.EnableDamageCollider();
        }
        public void OpenLeftDamageCollider()
        {
            leftDamageCollider.EnableDamageCollider();
        }
        public void CloseRightDamageCollider()
        {
            rightDamageCollider.DisableDamageCollider();
        }
        public void CloseLeftDamageCollider()
        {
            leftDamageCollider.DisableDamageCollider();
        }
        #endregion
    }
}
