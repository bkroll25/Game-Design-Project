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
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //cooldown for attacks
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        //if skeleton in range to attack
        if (distanceToPlayer < 1.5f)
        {
            m_body2d.velocity = new Vector2(0f, m_body2d.velocity.y);
            m_animator.SetBool("Moving", false);
            //if skeleton is attacking
            if (
                    !m_animator.GetCurrentAnimatorStateInfo(0).IsName("SkeletonAttack")
                    && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.8f//ensures trigger doesn't trip repeatedly
                )
            {
                hit = false;
                m_animator.Play("SkeletonAttack", 0, 0f);//play attack animation
            }
            //if more than halfway through motion register hit
            else if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .7f && !hit)
            {
                UnityEngine.Debug.Log("HEALTH LOST");
                hit = true;
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
}
