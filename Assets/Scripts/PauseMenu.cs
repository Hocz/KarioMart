using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PauseMenu : MonoBehaviour
{
    GameManager gameManager;
    public GameObject pauseMenu;

    [HideInInspector]
    public bool gameIsPaused;

    private void Start()
    {
        pauseMenu.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetGamePaused()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void SetGameResumed()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void SetGameRestartLevel()
    {
        Time.timeScale = 1f;
        SceneLoader.LoadScene(gameManager.currentScene);
        gameIsPaused = false;
    }

    public void SetGameToMainMenu()
    {
        Time.timeScale = 1f;
        SceneLoader.LoadScene(Scenes.MainMenu);
        gameIsPaused = false;
    }
}
