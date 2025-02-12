using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100; 
    int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
{
    currentHealth -= damage;

    //play hurt animation
    //animator.SetTrigger("Hurt"); 

    if (currentHealth <= 0)
    {
        Die();
    }
}

void Die()
{
    Debug.Log("Enemy died!");

    //play die animation
    //animator.SetBool("IsDeath", true);

    //disable the enemy
    GetComponent<Collider2D>().enabled = false;
    this.enabled = false; 
     //want enemies dissapear when they died 
      Destroy(gameObject); 
}
}
