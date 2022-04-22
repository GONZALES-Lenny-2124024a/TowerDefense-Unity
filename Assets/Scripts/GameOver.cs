using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text wavesText;
    
    void OnEnable () {  //Call when its enabled
        wavesText.text = PlayerStats.waves.ToString();
    }
    
    public void Retry () {
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);  //Gets the index of the active scene and load it
    }

    public void Menu () {
        Debug.Log("Menu");
    }
}
