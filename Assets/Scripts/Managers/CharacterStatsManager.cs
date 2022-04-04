using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    public class CharacterStatsManager : MonoBehaviour
    {
        [Header("Health")]
        public int healthLevel = 10;
        public float maxHealth; 
        public float currentHealth;

        public bool isDead = false;
    }
}
