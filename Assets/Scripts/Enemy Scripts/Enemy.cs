using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class Enemy : MonoBehaviour
    {
        public Animator animator;
        public float maxHealth = 100;
        float currentHealth;
        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
        }
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            //animator.SetTrigger("Hurt");
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            Debug.Log("Enemy died!");
            //die animation
            //animator.SetBool("isDead",true)
            //disable the enemy
            GetComponent<BoxCollider>().enabled = false;
            this.enabled = false;
        }
    }
}