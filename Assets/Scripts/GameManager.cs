using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameEnded; //To return only one "Game Over"
    
    public GameObject gameOverUI;

    void Start () {
        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded) { return; }
        
        if (PlayerStats.Lives <= 0) {
            EndGame();
        }
    }

    void EndGame() {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }
}
