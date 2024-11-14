using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class Enemy_Sensor_Bandit : MonoBehaviour
{

    private int m_leftColCount = 0;
    private int m_rightColCount = 0;

    private void OnEnable()
    {
        m_leftColCount = 0;
        m_rightColCount = 0;
    }

    public bool StateLeft()
    {
        return m_leftColCount > 0;
    }

    public bool StateRight()
    {
        return m_rightColCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.transform.position.x > transform.position.x)
            {
                m_rightColCount++;
            }
            else
            {
                m_leftColCount++;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.transform.position.x > transform.position.x)
            {
                m_rightColCount--;
            }
            else
            {
                m_leftColCount--;
            }
        }
    }

    void Update()
    {
        
    }
}
