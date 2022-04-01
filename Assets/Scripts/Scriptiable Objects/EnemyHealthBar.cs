using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    public Slider slider;
    public float timeUntilBarIsHidden = 0;
 

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        slider.gameObject.SetActive(false);

    }
    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    } 

    public void SetCurrentHealth(float currentHealth)
    {
        slider.value = currentHealth;
        timeUntilBarIsHidden = 3;
    }

    private void Update()
    {
        timeUntilBarIsHidden -= Time.deltaTime;

        if(timeUntilBarIsHidden <=0)
        {
            timeUntilBarIsHidden = 0;
            slider.gameObject.SetActive(false);
        }
        else
        {
            if(!slider.gameObject.activeInHierarchy)
            {
                slider.gameObject.SetActive(true);
            }
        }

        if(slider.value <= 0)
        {
            Destroy(slider.gameObject);
        }
    }
}
