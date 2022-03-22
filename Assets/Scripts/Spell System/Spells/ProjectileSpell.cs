using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


[CreateAssetMenu (fileName = "Projectile Spell", menuName = "Spells/Projectile Spell")]
public class ProjectileSpell : SpellItem
{
   [Header("Pojectile Damage")]
    public float baseDamage;

    [Header("Pojectile Physics")]
    public float projectileForwardVelocity;
    public float projectileUpwardVelocity;
    public bool isEffecteByGravity;
    public float projectileMass = 1;

    Rigidbody rb;
    PlayerController playerController;
    Camera cam;
    private void OnEnable()
    {
        cam = Camera.main;
    }
    

    public override void AttemptToCastSpell(AnimationHandler animationHandler, PlayerStats playerStats, WeaponSlotManager weaponSlot)
    {
        GameObject istantiateWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlot.rightHandSlot.transform);
        //istantiateWarmUpSpellFX.gameObject.transform.localScale = new Vector3(100, 100, 100);
        animationHandler.PlayTargetAnimation(spellAnimation, true);
    }

    public override void SuccessfullyCastSpell(AnimationHandler animationHandler, PlayerStats playerStats, WeaponSlotManager weaponSlot)
    {
        GameObject instantiateSpellFX = Instantiate(spellCastFx, weaponSlot.transform);
        //spelldamageCollider
        Vector3 aimSpot = cam.transform.position;
        aimSpot += cam.transform.forward * 50.0f;
        instantiateSpellFX.transform.LookAt(aimSpot);
        rb.AddForce(instantiateSpellFX.transform.forward * projectileForwardVelocity);
        rb.AddForce(instantiateSpellFX.transform.up * projectileUpwardVelocity);
        rb.useGravity = isEffecteByGravity;
        rb.mass = projectileMass;
        instantiateSpellFX.transform.parent = null;
    }
}
