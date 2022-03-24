using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Bramble : MonoBehaviour, IDamage 
{
    private void Awake()
    {

    }

    public void TakeDamage(float damage)
    {
        Destroy(gameObject);
    }

}
