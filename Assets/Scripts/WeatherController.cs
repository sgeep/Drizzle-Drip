using System;
using System.Collections;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public TypingEffect typingEffect;
    public string[] weatherEvents;
    public float delayBetweenEvents = 5.0f; // Time in seconds to wait before showing the next event
    private string currentWeatherEvent; // Updated to store the name of the current event
    public int score = 0; // Track the player's score
    public Scoring scoringSystem; // Reference to the scoring system class'
    private Coroutine weatherEventCoroutine; // Store a reference to the coroutine
    public UIManager uiManager; // Reference to the UIManager class

    private void Start()
    {
        // Start the sequence with an introductory message.
        //typingEffect.StartIntroSequence();
    }

    public void StartGame()
    {
        typingEffect.ResetTyping(); // Clear any existing text
        typingEffect.StartIntroSequence();
    }

    public void ResetGame() {
        if (weatherEventCoroutine != null) {
            StopCoroutine(weatherEventCoroutine);
            weatherEventCoroutine = null;
        }
        scoringSystem.ResetScore();
        StartGame();
    }

    public void OnIntroComplete()
    {
        if (weatherEventCoroutine != null)
        {
            StopCoroutine(weatherEventCoroutine);
        }
        weatherEventCoroutine = StartCoroutine(ShowRandomWeatherEvent());
    }

    IEnumerator ShowRandomWeatherEvent()
    {
        while (true) // Loop to continuously show weather events
        {
            if (weatherEvents.Length > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, weatherEvents.Length);
                currentWeatherEvent = weatherEvents[randomIndex];

                typingEffect.StartWeatherEventTyping(currentWeatherEvent);

                // Wait for the specified delay, then clear the text and show the next event
                yield return new WaitForSeconds(delayBetweenEvents + typingEffect.typingSpeed * currentWeatherEvent.Length);
            }
            else
            {
                Debug.LogError("No weather events specified in the WeatherController.");
                yield break; // Exit the coroutine if there are no events to display.
            }


        }
    }

    public void CheckWeatherEventMatch(string clickedWeatherType)
    {
        if (string.Equals(clickedWeatherType.Trim(), currentWeatherEvent.Trim(), StringComparison.OrdinalIgnoreCase)) {
        scoringSystem.CorrectGuess();
        }
        else {
        Debug.Log("Incorrect. Current event was: " + currentWeatherEvent);
        EndGame();
        }

        StartCoroutine(PrepareNextRound());
    }

    IEnumerator PrepareNextRound()
    {
    yield return new WaitForSeconds(2); // Adjust delay as needed
    scoringSystem.NewRound();
    OnIntroComplete(); // Or directly call ShowRandomWeatherEvent if you don't need the intro sequence again
    }

    public void EndGame()
    {
        uiManager.ShowGameOverScreen();
        // Additional logic to end the game
    }
}
