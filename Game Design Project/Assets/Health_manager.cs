using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_manager : MonoBehaviour
{
    public GameObject player;
    public Image healthBar;
    private int currentHealth;
    private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = player.GetComponent<PlayerHealth>().maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = player.GetComponent<PlayerHealth>().getCurrentHealth();
        healthBar.fillAmount = currentHealth / (float)maxHealth;
    }
    

}
