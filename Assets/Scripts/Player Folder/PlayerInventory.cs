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
        public Spell currentSpell;
        [InlineEditor]
        public ConsumableItem currentConsumable;

        public WeaponItem unarmedWeapon;

        public WeaponItem[] weaponInRightHandSlots = new WeaponItem[3];
        public WeaponItem[] weaponInLeftHandSlots = new WeaponItem[3];

        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1;
        public int currentSpellIndex = 0;
        public int consumableIndex = 0;
        public int slotAmounts = 0;
        public int itemAmounts = 0;

        QuickSlotsUI quickSlots;


        [InlineEditor]
        public List<Spell> spellSlots = new List<Spell>();

        [InlineEditor]
        public List<ConsumableItem> consumableItems = new List<ConsumableItem>();

        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            quickSlots = FindObjectOfType<QuickSlotsUI>();
            currentSpell = spellSlots[currentSpellIndex];
            quickSlots.UpdateSpellSlotUI(currentSpell);
            quickSlots.UpdateConsumableSlotUI(currentConsumable);
            slotAmounts = spellSlots.Count;
            itemAmounts = consumableItems.Count;
        }

        private void Start()
        {

            weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex], false);
            weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex], true);

            for (int r = 0; r < weaponInRightHandSlots.Length; r++)
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
                
                currentRightWeaponIndex = 0;
                rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex], false);
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
                currentLeftWeaponIndex = 0;
                leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex]; 
                weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex], true);
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
                quickSlots.UpdateSpellSlotUI(currentSpell);
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
        public void ChangeConsumables()
        {
            consumableIndex += 1;
            //Check if currentSpellIndex > spellSlots max array
            if (consumableIndex > itemAmounts - 1)
            {
                consumableIndex = 0;
            }
            //Check if currentSpellIndex is not null
            //false:make currentSpellIndex currentSpell
            if (consumableItems[consumableIndex] != null)
            {
                currentConsumable = consumableItems[consumableIndex];
                quickSlots.UpdateConsumableSlotUI(currentConsumable);
            }
            //true:move to the next Index
            else if (consumableItems[consumableIndex] == null)
            {
                consumableItems.RemoveAt(consumableIndex);
                consumableIndex += 1;
            }

        }
    }
}
