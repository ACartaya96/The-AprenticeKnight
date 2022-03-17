using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Stats")]
    public float maxHealth = 100;
    float currentHealth;
    public float maxMana = 100;
    float currentMana;
    public HealthBar healthBar;
    public ManaBar manaBar;

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;


        healthBar.setMaxHealth(maxHealth);
        manaBar.SetMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
