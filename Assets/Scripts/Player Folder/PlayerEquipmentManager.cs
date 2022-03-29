using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        BlockingColllider blockingColllider;
        PlayerInventory playerInventory;
        

        private void Awake()
        {
            
            blockingColllider = GetComponentInChildren<BlockingColllider>();
            playerInventory = GetComponentInParent<PlayerInventory>();
        }

        public void OpenBlockingCollider()
        {
            if(playerInventory.leftWeapon.weaponType == WeaponType.Shield)
            {
                blockingColllider.SetCollliderDamageAbsorption(playerInventory.leftWeapon);
            }
            else if(playerInventory.rightWeapon.weaponType == WeaponType.Shield)
            {
                blockingColllider.SetCollliderDamageAbsorption(playerInventory.rightWeapon);
            }
           
            blockingColllider.EnableBlockingCollider();
        }

        public void CloseBlockingCollider()
        {
            blockingColllider.DisableBlockingCollider();
        }
    }
}
