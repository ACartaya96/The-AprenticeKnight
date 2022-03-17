using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;
        //animator.SetTrigger("Hurt");
        if(currentHealth <= 0)
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
