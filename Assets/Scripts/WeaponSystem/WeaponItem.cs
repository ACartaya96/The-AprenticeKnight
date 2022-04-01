using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {Weapon, Shield, Staff}
namespace TAK
{
    [CreateAssetMenu(menuName = "Items/WeaponItem")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Damage")]
        public float baseDamage;

        [Header("Absorption")]
        public float physicalDamageAbsorption;

        [Header("RB Animations")]
        public string Right_Attack_1;
        public string Right_Attack_2;

        [Header("LB Animations")]
        public string Left_Attack_1;
        public string Left_Attack_2;

        [Header("RT Animations")]
        public string OH_Heavy_Attack_1;
        public string OH_Heavy_Attack_2;

        [Header("LT Special Attack")]
        public string LT_Special_Attack;

   

        [Header("Weapon Type")]
        public WeaponType weaponType;


    }
}
