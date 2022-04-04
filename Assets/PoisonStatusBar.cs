using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TAK
{
    public class PoisonStatusBar : MonoBehaviour
    {
        public Slider slider;
        public float timeUntilBarIsHidden = 0;


        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            slider.gameObject.SetActive(false);

        }

        public void setMaxPoisonBuildUp(float maxPoisonBuildUp)
        {
            slider.maxValue = maxPoisonBuildUp;


        }

        public void SetCurrentPoisonBuildUp(float currentPoisonBuildUp)
        {
            slider.value = currentPoisonBuildUp;
            timeUntilBarIsHidden = 3;
        }

        private void Update()
        {
            timeUntilBarIsHidden -= Time.deltaTime;

            if (timeUntilBarIsHidden <= 0)
            {
                timeUntilBarIsHidden = 0;
                slider.gameObject.SetActive(false);
            }
            else
            {
                if (!slider.gameObject.activeInHierarchy)
                {
                    slider.gameObject.SetActive(true);
                }
            }

         
        }
    }
}
