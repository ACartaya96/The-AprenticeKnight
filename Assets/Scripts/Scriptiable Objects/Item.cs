using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TAK
{
    public class Item : ScriptableObject
    {
        [HorizontalGroup("Item Infromation")]
        [PreviewField]
        public Sprite itemIcon;
        public string itemName;

    }
}
