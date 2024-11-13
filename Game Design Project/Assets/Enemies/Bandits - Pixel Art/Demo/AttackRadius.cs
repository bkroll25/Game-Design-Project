using UnityEngine;
using System.Collections;
using System.ComponentModel.Design;

public class AttackRadius : MonoBehaviour
{
    private int m_ColCount = 0;
    public int damage = 10;
    private bool active = false;

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
    }
    public void deactivate()
    {
        active = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log("Collision");
        m_ColCount++;
        if (active && other.CompareTag("Enemy"))
        {
            UnityEngine.Debug.Log("Hit");
            // Get the enemy's health and apply damage
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        m_ColCount--;
    }

    void Update()
    {

    }
}
