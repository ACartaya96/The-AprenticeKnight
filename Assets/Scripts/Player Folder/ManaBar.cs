using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TAK
{
    public class ManaBar : MonoBehaviour
    {

        public Slider slider;
        Canvas canvas;
        RectTransform rectTransform;
        private void Awake()
        {
            slider = GetComponent<Slider>();
            rectTransform = GetComponent<RectTransform>();
        }
        public void setMaxMana(float maxMana)
        {
            slider.maxValue = maxMana;
            slider.value = maxMana;
            //rectTransform.localScale = new Vector3(maxMana / 100, rectTransform.localScale.y, 0);
            rectTransform.anchorMax = new Vector3(rectTransform.anchorMax.x + (maxMana/2000), rectTransform.anchorMax.y, 0);
        }

        public void SetCurrentMana(float currentMana)
        {
            slider.value = currentMana;
        }

    }
}

