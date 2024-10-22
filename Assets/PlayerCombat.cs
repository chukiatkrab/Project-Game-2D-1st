using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour   
{
    public Animator animator;
    public Transform AttackPoint;
    public LayerMask EnemyLayers;

    public float Attackrange = 0.5f;
    public int attackDamage = 40;
    public float attackRate = 3f;
    float nextAttackTime = 0f;

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // play attack animation
        animator.SetTrigger("Attack");

        // detect enemy 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, Attackrange, EnemyLayers);
        
        //damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<enemy>().TakeDamage(attackDamage);   
        }
    }

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        {
            Gizmos.DrawWireSphere(AttackPoint.position, Attackrange); 
        }
    }
}
