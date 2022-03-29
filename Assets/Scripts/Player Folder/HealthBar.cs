using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TAK
{
    public class HealthBar : MonoBehaviour
    {

        public Slider slider;
        Canvas canvas;
        RectTransform rectTransform;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            rectTransform = GetComponent<RectTransform>();
        }
        public void setMaxHealth(float maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth; 
            rectTransform.anchorMax = new Vector3(rectTransform.anchorMax.x + (maxHealth / 5000), rectTransform.anchorMax.y, 0);
        }

        public void SetCurrentHealth(float currentHealth)
        {
            slider.value = currentHealth;
        }

    }
}
