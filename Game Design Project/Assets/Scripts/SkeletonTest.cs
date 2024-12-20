using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;

public class SkeletonTest : MonoBehaviour
{
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private bool hit;
    private EnemyHealth m_health;
    public Transform player;
    private EnemyAttackRadius m_attackRadius;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>(); 
        m_health = GetComponent<EnemyHealth>();
        m_attackRadius = transform.Find("EnemyAttackRadius").GetComponent<EnemyAttackRadius>();
    }

    // Update is called once per frame
    void Update()
    {
        //let animations play
        if (m_health.IsDead() || m_health.IsHit()) return;
        
        //cooldown for attacks
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        //if skeleton in range to attack
        if (distanceToPlayer < 1.5f)
        {
            m_body2d.velocity = new Vector2(0f, m_body2d.velocity.y);
            if (!hit)
            {
                m_animator.SetBool("Moving", false);
                m_animator.Play("SkeletonAttack", 0, 0f);
                Invoke("Attack", 0.4f);
                hit = true;
                Invoke("ResetHit", 1.2f);
            }
        }
        //if skeleton in range to follow
        else if (distanceToPlayer < 4)
        {
            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-2.0f, 2.0f, 2.0f);
                m_body2d.velocity = new Vector2(-2f, m_body2d.velocity.y);
                m_animator.SetBool("Moving", true);
            }
            else
            {
                transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                m_body2d.velocity = new Vector2(2f, m_body2d.velocity.y);
                m_animator.SetBool("Moving", true);
            }
        }
        else
        {
            m_body2d.velocity = new Vector2(0f, m_body2d.velocity.y);
            m_animator.SetBool("Moving", false);
        }
    }

    void Attack()
    {
        //prevent skeleton hitting player while hit/dead
        if (m_health.IsDead() || m_health.IsHit()) return;
        // Activate the attack collider (trigger hit detection)
        m_attackRadius.activate();

        // Optionally, deactivate it after a short delay (to prevent continuous hits)
        Invoke("DeactivateAttack", 0.5f); // 0.1 seconds for the duration of the attack hitbox
    }

    void DeactivateAttack()
    {
        // Deactivate the attack collider after the attack duration
        m_attackRadius.deactivate();
    }

    private void ResetHit()
    {
        hit = false;
    }

    public bool IsDeath()
    {
        return m_health.IsDead();
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }
}
