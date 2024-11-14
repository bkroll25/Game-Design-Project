using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class ChangeHandle : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController [] animations;
    private bool scanner = false;
    public GameObject player;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            scanner = true;
        }
    }
    
    void OnTriggerExit2D ( Collider2D collision )
    {
        if (collision.CompareTag("Player"))
        {
            scanner = false;
        }
    }

    private void Update()
    {
        if(scanner){
            if(Input.GetButtonDown("Fire3")){
                player.GetComponent<Bandit>().SwapState();
                GetComponent<Animator>().runtimeAnimatorController = animations[1];
                Destroy (gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); 
            }
        }
        
        
    }
    
}
