using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    public class EnemyStats : CharacterStatsManager
    {
       
        //[SerializeField] private int manaLevel = 10;
        
       // public float maxMana { get; private set; }
        //public float currentMana;


        public int enemyLevel;

        public HealthBar healthBar;
        //public ManaBar manaBar;
        

        EnemyManager enemyManager;
        EnemyAnimationHandler enemyAnimationHandler;
        EnemyMovement enemyMovement;

        private void Start()
        {
            enemyMovement = GetComponent<EnemyMovement>();
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();
            maxHealth = SetMaxHealthFromHealthLevel();
           // maxMana = SetMaxManafromManaLevel();
            currentHealth = maxHealth;
           // currentMana = maxMana;
            healthBar.setMaxHealth(maxHealth);
           // manaBar.setMaxMana(maxMana);
        }

        private float SetMaxHealthFromHealthLevel()
        {

            maxHealth = (healthLevel * 30);
            return maxHealth;
        }
       /* private float SetMaxManafromManaLevel()
        {
            maxMana = manaLevel * 5;
            return maxMana;
        }*/

        public void TakeDamage(float damage, string damageAnimation)
        {
           
                Debug.Log("Xander Takes " + damage.ToString() + " Damage");
                currentHealth -= damage;
                healthBar.SetCurrentHealth(currentHealth);
                Debug.Log("Xander HP: " + currentHealth.ToString());
                //playerController.rb.AddForce(-playerController.myTransform.forward * 20, ForceMode.Force);
                enemyAnimationHandler.PlayTargetAnimation(damageAnimation, true);
            
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                enemyAnimationHandler.PlayTargetAnimation("Dying", true);
            }
        }

        public void HealPlayer(float heal)
        {
            currentHealth = currentHealth + heal;

            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            healthBar.SetCurrentHealth(currentHealth);
        }

       /* public void UseMana(float manaCost)
        {
            Debug.Log("Mana Cost: " + manaCost.ToString());
            currentMana -= manaCost;
            manaBar.SetCurrentMana(currentMana);

            if (currentMana < 0)
            {
                currentMana = 0;
            }
            Debug.Log("CurrentMana: " + currentMana.ToString());

        }*/
    }
}