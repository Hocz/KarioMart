using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    GameManager gameManager;
    public GameObject winScreen;
    
    [HideInInspector]
    public bool raceIsFinished = false;
    
    void Start()
    {
        winScreen.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetGameToNextLevel()
    {
        gameManager.PlayersFinished();
        gameManager.LoadNext();
        Time.timeScale = 1f;
        raceIsFinished = false;
    }

    public void SetGameToMainMenu()
    {
        SceneLoader.LoadScene(Scenes.MainMenu);
        Time.timeScale = 1f;
        raceIsFinished = false;
    }

    public void EnableWinScreen()
    {
        winScreen.SetActive(true);
    }
}
