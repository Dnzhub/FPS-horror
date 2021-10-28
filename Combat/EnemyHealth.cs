using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth = 100f;

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken"); //OnDamageTaken a function in AIController script
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        if(currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponentInChildren<Animator>().SetTrigger("Death");
    }
}
