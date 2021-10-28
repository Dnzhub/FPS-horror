using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    DeathHandler DeathHandler;
    [SerializeField] float maxHealth = 200f;
    [SerializeField] float currentHealth;
    void Start()
    {
        DeathHandler = GetComponent<DeathHandler>();
        currentHealth = maxHealth;
    }
   

    public bool isDead()
    {
        return currentHealth <= 0;
    }
  
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        DeathHandler.MenuLoader();              
    }
}
