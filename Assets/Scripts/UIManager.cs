using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton instance

    public GameObject titleScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    // Keeps track of whether the game is currently active
    public bool IsGameActive { get; private set; }

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

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        IsGameActive = false; // Ensure the game state is not active
    }

    public void ShowPauseScreen()
    {
        if (IsGameActive)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0; // Pause the game
        }
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        pauseScreen.SetActive(false);
        titleScreen.SetActive(false);
        Time.timeScale = 0; // Pause the game
        IsGameActive = false;
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
        IsGameActive = true;
        Time.timeScale = 1; // Ensure the game is not paused
        Debug.Log("Game Started!");
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

    // Method to retry the game after a game over
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        Time.timeScale = 1; // Ensure the game is not paused
        IsGameActive = true;
    }

    // Method to quit to the title screen
    public void QuitToTitle()
    {
        ShowTitleScreen();
        // Reset game state as necessary
        // Optional: Load the title scene if it's a separate scene
        // SceneManager.LoadScene("TitleSceneName");
    }

    // End the game
    public void EndGame()
    {
        IsGameActive = false;
        ShowGameOverScreen();
        Debug.Log("Game Ended!");
    }
}
