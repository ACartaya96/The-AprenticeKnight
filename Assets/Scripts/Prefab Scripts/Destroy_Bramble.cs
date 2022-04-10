using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{


    public class Destroy_Bramble : MonoBehaviour
    {
        private void Awake()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            SpellObjectConfiguration spellObject = other.GetComponent<SpellObjectConfiguration>();
            if(spellObject != null && spellObject.spell.spellCategory == Spell.SpellCategory.Fire)
                Destroy(gameObject);

        }

    }
}
