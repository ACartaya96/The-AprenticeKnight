using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TAK
{
    public class HealthBar : MonoBehaviour
    {

        public Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();

        }
        public void setMaxHealth(float maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void SetCurrentHealth(float currentHealth)
        {
            slider.value = currentHealth;
        }

    }
}
