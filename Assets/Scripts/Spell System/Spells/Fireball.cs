using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fireball : MonoBehaviour
{
 
    

    
    private float timetoDestroy = 10f;
    public Vector3 target { get; set; }
    public bool hit { get; set; }
    public float speed = 10;
    public float damage = 10;


    Rigidbody rb;


    private void Awake()
    {
        Destroy(gameObject, timetoDestroy);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyControlSystem enemy = other.gameObject.GetComponent<EnemyControlSystem>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
