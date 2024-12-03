using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BringerOfDeathHealth : MonoBehaviour
{
    public int maxHealth;
    public Transform player;
    private Rigidbody2D m_body2d;
    private int currentHealth;
    private Animator m_animator;
    private float m_DisableTimer;
    private bool dying;
    private bool hit;
    private bool left_fixpos;
    System.Random rnd;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        dying = false;
        hit = false;
        currentHealth = maxHealth;
        rnd = new System.Random();
        left_fixpos = false;
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
                UnityEngine.Debug.Log("Hit Played");
                hit = true;
                if (rnd.Next(2) != 0)
                {
                    Invoke("ResetHit", 0.4f);

                    m_animator.SetTrigger("Hit");
                }
                else
                {
                    Invoke("Teleport", 0.2f);
                }
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

    private void Teleport()
    {
        UnityEngine.Debug.Log("Teleport");
        m_animator.SetTrigger("Teleport");
        Invoke("ChangePos", 0.9f);
    }

    private void ChangePos()
    {
        UnityEngine.Debug.Log("ChangePos");
        if (player.position.x < transform.position.x)
        {
            Vector2 currentPosition = m_body2d.position;
            currentPosition.x += 4f;
            currentPosition.y += .01f;
            m_body2d.position = currentPosition;
            left_fixpos = false;
        } 
        else
        {
            Vector2 currentPosition = m_body2d.position;
            currentPosition.x -= 4f;
            currentPosition.y += .01f;
            m_body2d.position = currentPosition;
            left_fixpos = true;
        }
        ResetHit();
        Invoke("FixPos", 1.3f);
    }

    private void FixPos()
    {
        if (left_fixpos)
        {
            Vector2 currentPosition = m_body2d.position;
            currentPosition.x += 0.7f;
            currentPosition.y += .01f;
            m_body2d.position = currentPosition;
        }
        else
        {
            Vector2 currentPosition = m_body2d.position;
            currentPosition.x -= 0.7f;
            currentPosition.y += .01f;
            m_body2d.position = currentPosition;
        }
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
