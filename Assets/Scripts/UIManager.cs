using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    // Use this for initialization
    void Start()
    {
        ShowTitleScreen();
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        pauseScreen.SetActive(false);
        titleScreen.SetActive(false);
        Time.timeScale = 0; // Pause the game, assuming it's not already paused
    }

    public void HideAllScreens()
    {
        titleScreen.SetActive(false);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }

    // Method to start the game from the title screen
    public void StartGame()
    {
        HideAllScreens(); // Hide all UI screens
        // Any additional logic to initialize the game state
        Time.timeScale = 1; // Ensure the game is not paused
    }

    // Method to resume the game from the pause screen
    public void ResumeGame()
    {
        pauseScreen.SetActive(false); // Hide the pause screen
        Time.timeScale = 1; // Resume the game
    }

    // Method to retry the game after a game over
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        Time.timeScale = 1; // Ensure the game is not paused
    }

    // Method to quit to the title screen
    public void QuitToTitle()
    {
        ShowTitleScreen(); // Show the title screen
        // Optional: Load the title scene if it's a separate scene
        // SceneManager.LoadScene("TitleSceneName");
        Time.timeScale = 1; // Ensure the game is not paused
    }
}