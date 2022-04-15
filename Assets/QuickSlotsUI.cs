using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TAK
{
    public class QuickSlotsUI : MonoBehaviour
    {
        public Image leftWeaponIcon;
        public Image rightWeaponIcon;
        public Image spellSlotIcon;
        public Image itemSlotIcon;

        public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weapon)
        {
            if(isLeft == false)
            {
                if(weapon.itemIcon != null)
                {
                    rightWeaponIcon.sprite = weapon.itemIcon;
                    rightWeaponIcon.enabled = true;
                }
                else
                {
                    rightWeaponIcon.enabled = false;
                }
           
            }
            else
            { 
                if (weapon.itemIcon != null)
                {
                    leftWeaponIcon.sprite = weapon.itemIcon;
                    leftWeaponIcon.enabled = true;
                }
                else
                {
                    leftWeaponIcon.enabled = false;
                }
            }
        }

        public void UpdateSpellSlotUI(Spell currentSpell)
        {
            if(currentSpell != null)
            {
                spellSlotIcon.sprite = currentSpell.itemIcon;
                spellSlotIcon.enabled = true;
            }
            else
            {
                spellSlotIcon.enabled = false;
            }
        }

        public void UpdateConsumableSlotUI(ConsumableItem item)
        {
            if (item != null)
            {
                itemSlotIcon.sprite = item.itemIcon;
                itemSlotIcon.enabled = true;
            }
            else
            {
                itemSlotIcon.enabled = false;
            }
        }
    }
}
