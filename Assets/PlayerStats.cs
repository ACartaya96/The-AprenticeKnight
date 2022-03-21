using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamage
{
    private int healthLevel = 10;
    private float maxHealth { get; set; }
    private float currentHealth { set; get; }

    public HealthBar healthBar;
    AnimationHandler animationHandler;
    public PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    private float SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetCurrentHealth(currentHealth);
        playerController.rb.AddForce(-playerController.myTransform.forward * 20, ForceMode.Force);
        //animationHandler.PlayTargetAnimation("Damage", true);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            animationHandler.PlayTargetAnimation("Dying", true);
        }
    }
}
