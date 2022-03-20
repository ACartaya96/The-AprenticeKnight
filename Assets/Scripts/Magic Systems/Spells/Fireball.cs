using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell
{
    //Ranged, at the start, max distance, require a target
    private const string spName = "Fireball";
    private const string spDescription = "A fiery mass that explodes on impact!";
    [SerializeField] private GameObject prefab;
    //private const Sprite icon = Resources.Load();

    [Header("Range")]
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private bool isRandom;
    [Header("Area of Effect")]
    [SerializeField] private float areaRadius;
    [SerializeField] private float effectDurationAOE;
    [SerializeField] private float baseEffectDamageAOE;
    [Header("Damage Over Time")]
    [SerializeField] private float effectDurationDOT;
    [SerializeField] private float baseEffectDamageDOT;
    [SerializeField] private float dotTick;
    public Fireball() : base(new BasicObjectInformation(spName, spDescription))
    {
        this.SpellBehaviors.Add(new Ranged(minDistance, maxDistance, isRandom));
        this.SpellBehaviors.Add(new AreaOfEffect(areaRadius, effectDurationAOE, baseEffectDamageAOE));
        this.SpellBehaviors.Add(new DamageOverTime(effectDurationDOT, baseEffectDamageDOT, dotTick));
    }
}
