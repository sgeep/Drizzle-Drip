using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  // Singleton instance
    public static GameManager Instance { get; private set; }

    // Keeps track of whether the game is currently active
    public bool IsGameActive { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, destroy myself.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the GameManager across scenes
        }
    }

    // Start the game
    public void StartGame()
    {
        IsGameActive = true;
        // Initialize game start logic here
        Debug.Log("Game Started!");
    }

    // End the game
    public void EndGame()
    {
        IsGameActive = false;
        // Initialize game end logic here
        Debug.Log("Game Ended!");
    }
}
