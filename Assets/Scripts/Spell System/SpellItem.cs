using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellItem : MonoBehaviour
{
    public GameObject spellWarUpFX;
    public GameObject spellCastFx;
    public string spellAnimation;


    [Header(("Spell Type"))]
    public SpellType type;
    public enum SpellType
    {
        Icantation,
        Sorcery
    }

    [Header("Spell Description")]
    [TextArea]
    public string spellDescription;

    public virtual void AttemptToCastSpell()
    {
        Debug.Log("You attemp to cast a spell!");
    }

    public virtual void SuccessfullyCastSpell()
    {
        Debug.Log("You successfully cast a spell!");
    }
}
