using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TAK
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;

        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;
        [InlineEditor]
        public SpellItem currentSpell;

        public WeaponItem unarmedWeapon;

        public WeaponItem[] weaponInRightHandSlots = new WeaponItem[3];
        public WeaponItem[] weaponInLeftHandSlots = new WeaponItem[3];

        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1;
        public int currentSpellIndex = 0;
        public int slotAmounts = 0;


        [InlineEditor]
        public List<SpellItem> spellSlots = new List<SpellItem>();

        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            currentSpell = spellSlots[currentSpellIndex];
            slotAmounts = spellSlots.Count;
        }

        private void Start()
        {
            rightWeapon = unarmedWeapon;
            leftWeapon = unarmedWeapon;

            for(int r = 0; r < weaponInRightHandSlots.Length; r++)
            {
                if(weaponInRightHandSlots[r] == null)
                {
                    weaponInRightHandSlots[r] = unarmedWeapon;
                }
            }

            for (int r = 0; r < weaponInRightHandSlots.Length; r++)
            {
                if (weaponInLeftHandSlots[r] == null)
                {
                    weaponInLeftHandSlots[r] = unarmedWeapon;
                }
            }
        }

        public void ChangeRightWeapon()
        {
            currentRightWeaponIndex += 1;

            if (currentRightWeaponIndex == 0 && weaponInRightHandSlots[0] != null)
            {
                rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex], false);
            }
            else if(currentRightWeaponIndex == 0 && weaponInRightHandSlots[0] == null)
            {
                currentRightWeaponIndex += 1;
            }

            
            else if (currentRightWeaponIndex == 1 && weaponInRightHandSlots[1] != null)
            {
                rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex], false);
            }
            else if(currentRightWeaponIndex == 1 && weaponInRightHandSlots[1] == null)
            {
                currentRightWeaponIndex += 1;
            }


            else if (currentRightWeaponIndex == 2 && weaponInRightHandSlots[2] != null)
            {
                rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex], false);
            }
            else if (currentRightWeaponIndex == 2 && weaponInRightHandSlots[2] == null)
            {
                currentRightWeaponIndex += 1;
            }

            if (currentRightWeaponIndex > weaponInRightHandSlots.Length - 1)
            {
                
                currentRightWeaponIndex = -1;
                rightWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
            }
         
        }

        public void ChangeLeftWeapon()
        {
            currentLeftWeaponIndex += 1;
            if (currentLeftWeaponIndex == 0 && weaponInLeftHandSlots[0] != null)
            {
               leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex], true);
            }
            else if (currentLeftWeaponIndex == 0 && weaponInLeftHandSlots[0] == null)
            {
                currentLeftWeaponIndex += 1;
            }


            else if (currentLeftWeaponIndex == 1 && weaponInLeftHandSlots[1] != null)
            {
                leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex], true);
            }
            else if(currentLeftWeaponIndex == 1 && weaponInLeftHandSlots[1] != null)
            {
                currentLeftWeaponIndex += 1;
            }

              else if (currentLeftWeaponIndex == 2 && weaponInLeftHandSlots[2] != null)
            {
                leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex], true);
            }
            else if(currentLeftWeaponIndex == 2 && weaponInLeftHandSlots[2] == null)
            {
                currentLeftWeaponIndex += 1;
            }

            if (currentLeftWeaponIndex > weaponInLeftHandSlots.Length - 1)
            {
                currentLeftWeaponIndex = -1;
                leftWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, true);
            }

           
        }

        public void ChangeSpells()
        {
            currentSpellIndex += 1;
            //Check if currentSpellIndex > spellSlots max array
            if (currentSpellIndex > slotAmounts - 1)
            {
                currentSpellIndex = 0;
            }
            //Check if currentSpellIndex is not null
            //false:make currentSpellIndex currentSpell
            if (spellSlots[currentSpellIndex] != null)
            {
                currentSpell = spellSlots[currentSpellIndex];
            }
            //true:move to the next Index
            else if (spellSlots[currentSpellIndex] == null)
            {
                spellSlots.RemoveAt(currentSpellIndex);
                currentSpellIndex += 1;
            }
             
             
         
            //true:move back to First Index
            //False:Do nothing
        }
    }
}
