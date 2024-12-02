using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public void SwapSceneRetry(){
        SceneManager.LoadScene("game", LoadSceneMode.Single);

    }
    public void SwapSceneDeath(){
        SceneManager.LoadScene("Death", LoadSceneMode.Single);
    }
    
}
