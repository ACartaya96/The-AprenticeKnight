using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TAK
{
    public class PlayerStats : CharacterStatsManager, IDamage
    {
 
        [SerializeField] private int manaLevel = 10;
 
        public float maxMana { get; private set; }
        public float currentMana;

        public HealthBar healthBar;
        public ManaBar manaBar;
        public int playerLevel;
        PlayerManager playerManager;
        AnimationHandler animationHandler;
        PlayerAudioManager playerAudioManager;
        public PlayerController playerController;

        [SerializeField] AudioClip impactClip;

        private void Start()
        {
            playerController = GetComponent<PlayerController>();
            playerManager = GetComponent<PlayerManager>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
            playerAudioManager = GetComponentInChildren<PlayerAudioManager>();
            maxHealth = SetMaxHealthFromHealthLevel();
            maxMana = SetMaxManafromManaLevel();
            currentHealth = maxHealth;
            currentMana = maxMana;
            healthBar.setMaxHealth(maxHealth);
            manaBar.setMaxMana(maxMana);
            SceneLoader.currentLevel = SceneManager.GetActiveScene().buildIndex;
        }

        private float SetMaxHealthFromHealthLevel()
        {
            
            maxHealth = (healthLevel * 30);
            return maxHealth;
        }
        private float SetMaxManafromManaLevel()
        {
            maxMana = manaLevel * 5;
            return maxMana;
        }

        

        public void TakeDamage(float damage, string damageAnimation)
        {
            if (!playerManager.isInvincible)
            {
                Debug.Log("Xander Takes " + damage.ToString() + " Damage");
                currentHealth -= damage;
                healthBar.SetCurrentHealth(currentHealth);
                Debug.Log("Xander HP: " + currentHealth.ToString());
                //playerController.rb.AddForce(-playerController.myTransform.forward * 20, ForceMode.Force);
                if(damageAnimation != null)
                {
                    animationHandler.PlayTargetAnimation(damageAnimation, true, false);
                    playerAudioManager.PlayTargetSoundEffect(impactClip);
                }
                    
            }
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                playerManager.isInvincible = true;
                animationHandler.PlayTargetAnimation("Dying", true, false);


                SceneLoader.currentLevel = SceneManager.GetActiveScene().buildIndex;
                SceneLoader.instance.Invoke("LoadLoseScene", 3f);
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
           if(playerManager.InfMana != true)
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

        public void RecoverMana(float manaCost)
        {
            currentMana += manaCost;
            manaBar.SetCurrentMana(currentMana);

            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
        }
    }
}
