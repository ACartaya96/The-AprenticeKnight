using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamage
{
    private int healthLevel = 10;
    private int manaLevel = 10;
    public float maxHealth { get; private set; }
    public float currentHealth; 
    public float maxMana { get; private set; }
    public float currentMana;

    public HealthBar healthBar;
    public ManaBar manaBar;
    PlayerManager playerManager;
    AnimationHandler animationHandler;
    public PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerManager = GetComponent<PlayerManager>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        maxHealth = SetMaxHealthFromHealthLevel();
        maxMana = SetMaxManafromManaLevel();
        currentHealth = maxHealth;
        currentMana = maxMana;
        healthBar.setMaxHealth(maxHealth);
        manaBar.setMaxMana(maxMana);
    }

    private float SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }
    private float SetMaxManafromManaLevel()
    {
        maxMana = manaLevel * 5;
        return maxMana;
    }

    public void TakeDamage(float damage)
    {
        if (playerManager.isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetCurrentHealth(currentHealth);
            playerController.rb.AddForce(-playerController.myTransform.forward * 20, ForceMode.Force);
            //animationHandler.PlayTargetAnimation("Damage", true);
        }
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            animationHandler.PlayTargetAnimation("Dying", true);
        }
    }

    public void HealPlayer(float heal)
    {
        currentHealth = currentHealth + heal;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthBar.SetCurrentHealth(currentHealth);
    }

    public void UseMana(float manaCost)
    {
        Debug.Log("Mana Cost: " + manaCost.ToString());
        currentMana -= manaCost;
        manaBar.SetCurrentMana(currentMana);

        if (currentMana < 0)
        {
            currentMana = 0;
        }
        Debug.Log("CurrentMana: " + currentMana.ToString());
       
    }
}
