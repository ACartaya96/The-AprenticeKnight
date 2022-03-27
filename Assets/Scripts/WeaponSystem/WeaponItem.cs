using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {Weapon, Shield, Staff}

[CreateAssetMenu(menuName = "Items/WeaponItem")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("Cast Point")]
    

    [Header("One Hand Attack Melee Animations")]
    public string OH_Light_Attack_1;
    public string OH_Light_Attack_2;
    public string OH_Heavy_Attack_1;
    public string OH_Heavy_Attack_2;

    [Header("Damage Detection")]
    public float meleeDamage;

    [Header("Weapon Type")]
    public WeaponType weaponType;
  

}
