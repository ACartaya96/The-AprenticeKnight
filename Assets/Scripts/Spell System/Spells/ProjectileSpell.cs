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

    public override void SuccessfullyCastSpell(AnimationHandler animationHandler, PlayerStats playerStats, WeaponSlotManager weaponSlot,
        PlayerManager playerManager, PlayerTargetDetection playerTarget)
    {
        GameObject instantiateSpellFX = Instantiate(spellCastFx, weaponSlot.rightHandSlot.transform.position, weaponSlot.rightHandSlot.transform.rotation);
        Destroy(instantiateSpellFX, 8f);
        Debug.Log(instantiateSpellFX.transform.position.ToString());
        //spelldamageCollider

  

        //Set Physics
        rb = instantiateSpellFX.GetComponent<Rigidbody>();
        
        DamageCollider damageCollider = instantiateSpellFX.GetComponent<DamageCollider>();
        damageCollider.EnableDamageCollider();




        if (playerTarget.currentLockedOnTarget != null)
        {
            Vector3 dir = playerTarget.currentLockedOnTarget.position - instantiateSpellFX.transform.position;
            
            dir.Normalize();

            Quaternion tr = Quaternion.LookRotation(dir);
            Quaternion targetRotation = Quaternion.Slerp(instantiateSpellFX.transform.rotation, tr, projectileForwardVelocity * Time.deltaTime);
            instantiateSpellFX.transform.rotation = targetRotation;
            instantiateSpellFX.transform.position = Vector3.MoveTowards(instantiateSpellFX.transform.position, playerTarget.currentLockedOnTarget.transform.position, projectileForwardVelocity * Time.deltaTime/5f);

        }
        else
        {
            Vector3 aimSpot = cam.transform.position;
            aimSpot += cam.transform.forward * 50;
            instantiateSpellFX.transform.LookAt(aimSpot);

        }





        rb.AddForce(instantiateSpellFX.transform.forward * projectileForwardVelocity );
        rb.AddForce(instantiateSpellFX.transform.up * projectileUpwardVelocity);

        
        Debug.Log(instantiateSpellFX.transform.position.ToString());
        rb.useGravity = isEffecteByGravity;
        rb.mass = projectileMass;
        instantiateSpellFX.transform.parent = null;

        //Damage and Cost
        

        playerStats.UseMana(cost);
    }

}
