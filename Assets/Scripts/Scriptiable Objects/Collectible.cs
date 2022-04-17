using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    [CreateAssetMenu(menuName = "Items/Collectibles")]
    public class Collectible : Item
    {
        [Header("Quantity")]
        public int maxAmount;
        public int currentAmount;

        
    }
}
