using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum SpellState
{
    Beggining,
    Middle,
    End
}

namespace TAK
{
    public class SpellBehaviors : ScriptableObject
    {
        [VerticalGroup("Game Data", 75)]
        [PreviewField(75)]
        public GameObject spellCastFx;

        public SpellBehaviors baseReference;
        public SpellItem spell;

        SpellCollider spellCollider;
        
        public virtual void OnActivateEffect(SpellBehaviors spellBase, WeaponSlotManager weaponSlots)
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }

        private void OnTriggerStay(Collider other)
        {
            
        }

        private void OnTriggerExit(Collider other)
        {
            
        }

    }
}
