using System.Collections;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public TypingEffect typingEffect;
    public string[] weatherEvents;
    public float delayBetweenEvents = 5.0f; // Time in seconds to wait before showing the next event
    private string currentWeatherEvent; // Updated to store the name of the current event
    public int score = 0; // Track the player's score

    private void Start()
    {
        // Start the sequence with an introductory message.
        typingEffect.StartIntroSequence();
    }

    public void OnIntroComplete()
    {
        // Start showing weather events after the intro is complete.
        StartCoroutine(ShowRandomWeatherEvent());
    }

    IEnumerator ShowRandomWeatherEvent()
    {
        while (true) // Loop to continuously show weather events
        {
            if (weatherEvents.Length > 0)
            {
                int randomIndex = Random.Range(0, weatherEvents.Length);
                string selectedEvent = weatherEvents[randomIndex];

                typingEffect.StartWeatherEventTyping(selectedEvent);

                // Wait for the specified delay, then clear the text and show the next event
                yield return new WaitForSeconds(delayBetweenEvents + typingEffect.typingSpeed * selectedEvent.Length);
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
        if (clickedWeatherType == currentWeatherEvent)
        {
            score++;
            Debug.Log("Correct! Score: " + score);
            // Optionally, show feedback to the player for a correct guess
        }
        else
        {
            Debug.Log("Incorrect. Current event was: " + currentWeatherEvent);
            // Optionally, handle incorrect guesses (e.g., score penalty, feedback to the player)
        }

        // Trigger next weather event display, or handle game progression
    }
}
