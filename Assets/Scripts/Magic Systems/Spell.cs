using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spell : MagicSystem
{
    private BasicObjectInformation objectInfo;
    private List<SpellBehaviors> behaviors;
    private bool requiresTarget;
    private bool canCastOnSelf;
    private float cooldown; //secs
    private GameObject spellPrefab;
    private float castTime; //secs
    private float cost;
    private SpellType type;

    public enum SpellType
    {
        Ranged,
        AoE,
        Effect
    }


    public Spell(BasicObjectInformation aBasicInfo)
    {
        objectInfo = aBasicInfo;
        behaviors = new List<global::SpellBehaviors>();
        cooldown = 0f;
        requiresTarget = false;
        canCastOnSelf = false;
    }

    public Spell(BasicObjectInformation aBasicInfo, List<SpellBehaviors> abehaviors)
    {
        objectInfo = aBasicInfo;
        behaviors = new List<SpellBehaviors>();
        behaviors = abehaviors;
        cooldown = 0f;
        requiresTarget = false;
        canCastOnSelf = false;
    }

    public Spell(BasicObjectInformation aBasicInfo, List<SpellBehaviors> abehaviors, bool arequireTarget, float acooldown, GameObject aSpellPrefab)
    {
        objectInfo = aBasicInfo;
        behaviors = new List<SpellBehaviors>();
        behaviors = abehaviors;
        cooldown = acooldown;
        requiresTarget = arequireTarget;
        canCastOnSelf = false;
        spellPrefab = aSpellPrefab;
    }

   
    public BasicObjectInformation SpellInfo
    {
        get { return objectInfo; }
    }

    public float SpellCooldown
    {
        get { return cooldown; }
    }

    public List<SpellBehaviors> SpellBehaviors
    {
        get { return behaviors; }
    }

    public void UseSpell()
    {

    }


}
