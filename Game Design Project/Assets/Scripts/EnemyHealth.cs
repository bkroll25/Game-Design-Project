using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    private Animator m_animator;
    private float m_DisableTimer;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (m_DisableTimer > 0f)
        {
            m_DisableTimer -= 0.05f;
        }
    }

    public void TakeDamage(int damage)
    {
        if (m_DisableTimer == 0f)
        {
            UnityEngine.Debug.Log("Damage");
            currentHealth -= damage;
            m_DisableTimer = 3f;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        m_animator.SetTrigger("Death");
        UnityEngine.Debug.Log("Skeleton has died!");
        Destroy(gameObject);
    }
}
