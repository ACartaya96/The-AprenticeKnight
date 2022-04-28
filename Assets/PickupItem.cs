using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class PickupItem : MonoBehaviour,IInteractable
    {
        public Collectible collectible;
        public BooleanContainer pickedUpItem;

        private void Awake()
        {
            if(collectible.currentAmount >= collectible.maxAmount || pickedUpItem.check)
            {
                Destroy(gameObject);
            }
        }

        public void Interact()
        {
            collectible.currentAmount += 1;
            pickedUpItem.check = true;

            if (collectible.currentAmount > 3)
            {
                collectible.currentAmount = 3;
            }

            Destroy(gameObject);
        }
    }
}
