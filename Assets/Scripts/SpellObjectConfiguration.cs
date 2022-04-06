using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class SpellObjectConfiguration : MonoBehaviour
    {
        [SerializeField] CharacterManager mySelf = null;
        public string selfReference;
        public Spell spell = null;
        public CharacterManager myTarget = null;

        private void Awake()
        {
            mySelf = GameObject.FindGameObjectWithTag(selfReference).GetComponent<CharacterManager>();
            spell = (Spell)Resources.Load("Spells/" + mySelf.gameObject.name, typeof(Spell));

            if (spell != null)
            {
                if (spell.spellType == Spell.SpellType.Single)
                {
                    if(spell.spellDirection == Spell.SpellDirection.Point)
                    {
                        CharacterEffectManager damageByEfect = myTarget.gameObject.GetComponent<CharacterEffectManager>();

                        if(spell.spellEffect == Spell.SpellEffect.DamagePerSecond)
                        {
                            myTarget.gameObject.GetComponent<IDamage>().TakeDamage(spell.spellMaxDamage, "Damage");
                        }

                    }
                }
            }
        }

    }
}
