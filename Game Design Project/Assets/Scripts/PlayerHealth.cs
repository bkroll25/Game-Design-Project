using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator m_animator;
    private float m_DisableTimer;
    private bool dying;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        dying = false;
    }

    void Update()
    {
        if (m_DisableTimer > 0f)
        {
            m_DisableTimer -= 0.02f;
        }

        if(currentHealth <= 0){
            SceneManager.LoadScene("Death", LoadSceneMode.Single);
        }
    }

    public void TakeDamage(int damage)
    {
        if (m_DisableTimer <= 0f)
        {
            currentHealth -= damage;
            UnityEngine.Debug.Log("Player Health: " + currentHealth);
            m_DisableTimer = 7f;
            if (currentHealth <= 0)
            {
                m_animator.SetTrigger("Death");
                dying = true;
                Invoke("Die", .5f);
            }
            else
            {
                m_animator.SetTrigger("Hurt");
            }
        }
    }

    void Die()
    {
        UnityEngine.Debug.Log("Skeleton has died!");
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return dying;
    }

    public int getCurrentHealth(){
        return currentHealth;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Death Barrier")){
            currentHealth = 0;
        }
    }
}
