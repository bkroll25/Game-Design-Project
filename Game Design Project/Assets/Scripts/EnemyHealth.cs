using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    private Animator m_animator;
    private float m_DisableTimer;
    private bool dying;
    private bool hit;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        dying = false;
        hit = false;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (m_DisableTimer > 0f)
        {
            m_DisableTimer -= 0.02f;
        }
    }

    public void TakeDamage(int damage)
    {
        if (m_DisableTimer <= 0f)
        {
            currentHealth -= damage;
            UnityEngine.Debug.Log("Enemy Health: " + currentHealth);
            m_DisableTimer = 5f;
            if (currentHealth <= 0)
            {
                m_animator.SetBool("Dying", true);
                dying = true;
                Invoke("Die", 1.28f);
            }
            else
            {
                m_animator.SetTrigger("Hit");
                UnityEngine.Debug.Log("Hit Played");
                hit = true;
                Invoke("ResetHit", 0.4f);
            }
        }
    }

    void Die()
    {
        UnityEngine.Debug.Log("Skeleton has died!");
        Destroy(gameObject);
    }

    private void ResetHit()
    {
        hit = false; 
    }

    public bool IsDead()
    {
        return dying;
    }

    public bool IsHit()
    {
        return hit;
    }
}
