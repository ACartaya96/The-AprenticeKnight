using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    public class EnemyStats : CharacterStatsManager, IDamage
    {
       
        //[SerializeField] private int manaLevel = 10;
        
       // public float maxMana { get; private set; }
        //public float currentMana;


        public int enemyLevel;

        //public HealthBar healthBar;
        //public ManaBar manaBar;
        

        EnemyManager enemyManager;
        EnemyAnimationHandler enemyAnimationHandler;
        EnemyMovement enemyMovement;

        public EnemyHealthBar enemyHealthBar;

        private void Start()
        {
            enemyMovement = GetComponent<EnemyMovement>();
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();
            maxHealth = SetMaxHealthFromHealthLevel();
           // maxMana = SetMaxManafromManaLevel();
            currentHealth = maxHealth;
           // currentMana = maxMana;
            enemyHealthBar.SetMaxHealth(maxHealth);
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
           if(!enemyManager.isInvincible)
            {
                Debug.Log("Hag Takes " + damage.ToString() + " Damage");
                currentHealth -= damage;
                enemyHealthBar.SetCurrentHealth(currentHealth);
                Debug.Log("Hag HP: " + currentHealth.ToString());
                //playerController.rb.AddForce(-playerController.myTransform.forward * 20, ForceMode.Force);
                enemyAnimationHandler.PlayTargetAnimation(damageAnimation, true,false);

            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                enemyManager.isInvincible = true;
                enemyAnimationHandler.PlayTargetAnimation("Dying", true,false);
                Destroy(gameObject, 3f);
            }
        }

        public void HealPlayer(float heal)
        {
            currentHealth = currentHealth + heal;

            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            //healthBar.SetCurrentHealth(currentHealth);
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