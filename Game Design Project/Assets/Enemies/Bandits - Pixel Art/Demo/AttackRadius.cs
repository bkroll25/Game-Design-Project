using UnityEngine;
using System.Collections;
using System.ComponentModel.Design;
using System.Collections.Generic;

public class AttackRadius : MonoBehaviour
{
    private int m_ColCount = 0;
    public int damage = 10;
    private bool active = false;
    List<Collider2D> collisions = new List<Collider2D>();

    private void OnEnable()
    {
        m_ColCount = 0;
    }

    public bool State()
    {
        return m_ColCount > 0;
    }

    public void activate()
    {
        active = true;
        for (int i = 0; i < collisions.Count; i++)
        {
            EnemyHealth enemyHealth = collisions[i].GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            BringerOfDeathHealth bossHealth = collisions[i].GetComponent<BringerOfDeathHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }
        }
    }
    public void deactivate()
    {
        active = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        m_ColCount++;
        if (other.CompareTag("Enemy"))
        {
            collisions.Add(other);
            if (active)
            {
                // Get the enemy's health and apply damage
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        m_ColCount--;
        for (int i = 0; i < collisions.Count; i++)
        {
            if (collisions[i] == other)
            {
                collisions.RemoveAt(i);
            }
        }
    }

    void Update()
    {

    }
}
