using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damage = 10f;

    private void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }
    private void Hit()
    {        
        if (target == null) return;
        target.TakeDamage(damage);
        target.GetComponent<DamageDisplay>().bloodEffectActivator();
    }
}
