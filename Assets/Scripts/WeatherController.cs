using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public TypingEffect typingEffect;
    public List<WeatherEvent> weatherEvents;
    public float delayBetweenEvents = 5.0f; // Time in seconds to wait before showing the next event
    private WeatherEvent currentWeatherEvent; // Updated to store the current event
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
            if (weatherEvents.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, weatherEvents.Count);
                currentWeatherEvent = weatherEvents[randomIndex];

                string preview = currentWeatherEvent.GetRandomPreview();
                typingEffect.StartWeatherEventTyping(preview);

                // Wait for the specified delay, then clear the text and show the next event
                yield return new WaitForSeconds(delayBetweenEvents + typingEffect.typingSpeed * preview.Length);
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
        if (string.Equals(clickedWeatherType.Trim(), currentWeatherEvent.Name.Trim(), StringComparison.OrdinalIgnoreCase)) {
            scoringSystem.CorrectGuess();
        }
        else {
            Debug.Log("Incorrect. Current event was: " + currentWeatherEvent.Name);
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

[System.Serializable]
public class WeatherEvent
{
    public string Name;
    public List<string> Previews = new List<string>();

    public string GetRandomPreview()
    {
        int index = UnityEngine.Random.Range(0, Previews.Count);
        return Previews[index];
    }
}