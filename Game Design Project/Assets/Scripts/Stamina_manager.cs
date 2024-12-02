using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina_manager : MonoBehaviour
{
    public GameObject player;
    public Image staminaBar;
    private int stamina;
    private int maxStamina;

    // Start is called before the first frame update
    void Start()
    {
        maxStamina = player.GetComponent<Bandit>().max_stamina;
    }

    // Update is called once per frame
    void Update()
    {
        stamina = player.GetComponent<Bandit>().stamina;
        staminaBar.fillAmount = stamina / (float)maxStamina;
    }
    

}
