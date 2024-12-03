using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    public GameObject player;
    private EnemyAttackRadius m_attackRadius;
    private BoxCollider2D m_boxCollider;
    private bool dead;
    private bool left;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_boxCollider = GetComponent<BoxCollider2D>();
        m_attackRadius = transform.Find("EnemyAttackRadius").GetComponent<EnemyAttackRadius>();
        dead = false;
        player = GameObject.FindWithTag("Player");
        left = (player.transform.position.x < transform.position.x);
        if (left) transform.eulerAngles = new Vector3(0, 0, 270f);
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 90f);
            m_boxCollider.offset = new Vector2(-1f * m_boxCollider.offset.x, m_boxCollider.offset.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //let animations play
        if (dead) return;

        if (left)
        {
            m_body2d.velocity = new Vector2(-3f, 0f);
        }
        else
        {
            m_body2d.velocity = new Vector2(3f, 0f);
        }

        //cooldown for attacks
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        //if skeleton in range to attack
        if (distanceToPlayer < 1.5f)
        {
            m_animator.SetTrigger("Attack");
            Invoke("Attack", 0.4f);
            dead = true;
        }
        else if (distanceToPlayer > 20)
        {
            dead = true;
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        // Activate the attack collider (trigger hit detection)
        m_attackRadius.activate();

        // Optionally, deactivate it after a short delay (to prevent continuous hits)
        Invoke("DeactivateAttack", 0.5f); // 0.1 seconds for the duration of the attack hitbox
    }

    void DeactivateAttack()
    {
        // Deactivate the attack collider after the attack duration
        m_attackRadius.deactivate();
        dead = true;
        Destroy(gameObject);
    }
}
