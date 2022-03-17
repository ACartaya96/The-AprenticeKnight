using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    [Space]
    public int attackDamage = 40;
    public float attackRange = 0.5f;
    [Space]
    public float attackRate = 3f;
    float nextAttackTime = 0f;
    [Space]
    public LayerMask EnemiesLayer;
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if(Input.GetButton("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        //play an attack animation
        //animator.SetTrigger("Attack")

        //Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(AttackPoint.position, attackRange, EnemiesLayer);

        //Damage them
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
    void onDrawGizmosSelected()
    {
        if(AttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
}
