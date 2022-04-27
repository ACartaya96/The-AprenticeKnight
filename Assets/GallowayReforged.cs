using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class GallowayReforged : MonoBehaviour
    {
        public Collectible collectible;
        PlayerInventory playerInventory;
        public WeaponItem weaponItem;
        public GameObject stageOne;
        public GameObject stageTwo;
        public GameObject stageThree;

        private void Awake()
        {
            //playerInventory = FindObjectOfType<PlayerInventory>();
            if(collectible.currentAmount == 1)
            {
                
               /*for(int i = 0; i < playerInventory.weaponInRightHandSlots.Length; i++)
                {
                    if(playerInventory.weaponInRightHandSlots[i] == null || playerInventory.weaponInRightHandSlots[i] == playerInventory.weaponInRightHandSlots[i].isUnarmed)
                    {
                        playerInventory.weaponInRightHandSlots[i] = weaponItem;
                        break;
                    }
                }*/
                stageOne.SetActive(true);
                weaponItem.baseDamage = 1;
            }
            else if(collectible.currentAmount == 2)
            {
                stageOne.SetActive(true);
                stageTwo.SetActive(true);
                weaponItem.baseDamage = 50;
            }
            else if(collectible.currentAmount >= 3)
            {
                stageOne.SetActive(true);
                stageTwo.SetActive(true);
                stageThree.SetActive(true);
                weaponItem.baseDamage = 100;
            }
            else
            {
                stageOne.SetActive(false);
                stageTwo.SetActive(false);
                stageThree.SetActive(false);
            }
        }
        // Update is called once per frame
        void Update()
        {
        }
    }
}

