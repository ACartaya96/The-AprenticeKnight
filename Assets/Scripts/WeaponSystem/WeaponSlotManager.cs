using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    WeaponHolderSlot leftHandSlot;
    WeaponHolderSlot rightHandSlot;

    DamageCollider leftDamageCollider;
    DamageCollider rightDamageCollider;

    private void Awake()
    {
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if(weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if(weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if(isLeft)
        {
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeaponDamageCollider();
        }
        else
        {
            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightWeaponDamageCollider();
        }
    }
    #region Wepaon's Damage Collider
    private void LoadLeftWeaponDamageCollider()
    {
        leftDamageCollider = leftHandSlot.currentWeapon.GetComponentInChildren<DamageCollider>();
    }

    private void LoadRightWeaponDamageCollider()
    {
        leftDamageCollider = rightHandSlot.currentWeapon.GetComponentInChildren<DamageCollider>();
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
