using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/WeaponItem")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("One Hand Attack Melee Animations")]
    public string OH_Light_Attack_1;
    public string OH_Heavy_Attack_1;

    [Header("Damage Detection")]
    public float meleeDamage;
}
