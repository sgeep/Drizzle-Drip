using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton instance

    public GameObject titleScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;


    // Keeps track of whether the game is currently active
    public bool IsGameActive { get; private set; }
    public WeatherController weatherController;

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optionally keep the UIManager across scenes
        }
    }

    void Start()
    {
        ShowTitleScreen();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Spacebar pressed");
            TogglePauseGame();
        }
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        weatherController.gameObject.SetActive(false);
        IsGameActive = false; // Ensure the game state is not active
        weatherController.ResetGame();
    }

    public void ShowPauseScreen()
    {
        if (IsGameActive)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0; // Pause the game

        }
        Debug.Log("Pausing game");
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        pauseScreen.SetActive(false);
        titleScreen.SetActive(false);
        IsGameActive = false;


        Time.timeScale = 0; // Pause the game
    }

    public void HideAllScreens()
    {
        titleScreen.SetActive(false);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    // Method to start the game from the title screen
    public void StartGame()
    {
        HideAllScreens();
        weatherController.gameObject.SetActive(true);
        weatherController.StartGame();
        Time.timeScale = 1; // Ensure the game is not paused
        IsGameActive = true;
        Debug.Log("Game Started!");
    }

    public void TogglePauseGame() {
        // If game is not active or gameOverScreen is active, ignore pause command
        if (!IsGameActive || gameOverScreen.activeSelf) {
            return;
        }

        // Check if the game is currently paused
        if (Time.timeScale == 1) {
            // Game is running, so pause it
            ShowPauseScreen();
        } else {
            // Game is paused, so resume it
            ResumeGame();
        }
    }

    // Method to resume the game from the pause screen
    public void ResumeGame()
    {
        if (IsGameActive)
        {
            pauseScreen.SetActive(false); // Hide the pause screen
            Time.timeScale = 1; // Resume the game
        }
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        Time.timeScale = 1; // Ensure the game is not paused
        IsGameActive = true;
        HideAllScreens();
        weatherController.ResetGame();
    }

    // Method to quit to the title screen
    public void QuitToTitle()
    {
        ShowTitleScreen();
        // Reset game state as necessary
    }

    // End the game
    public void EndGame()
    {
        IsGameActive = false;
        ShowGameOverScreen();
        Debug.Log("Game Ended!");
    }
}
