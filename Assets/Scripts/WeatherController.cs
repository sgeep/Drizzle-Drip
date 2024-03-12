using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeatherController : MonoBehaviour
{
    public TextMeshProUGUI weatherText; // Reference to the displayed weather text
    public TypingEffect typingEffect; // Reference to the TypingEffect script

    [System.Serializable]
    public class WeatherEvent
    {
        public string name; // Name of the weather event
        public GameObject visualEffect; // The visual effect prefab for the weather
        public float duration; // How long this weather event lasts
    }

    public WeatherEvent[] weatherEvents; // Array to hold different weather events

    // Start the next weather event

       private void Start()
    {
        StartNextEvent(); // Automatically start the next event when the game starts
    }

    public void StartNextEvent()
    {
        if (weatherEvents.Length == 0)
        {
            Debug.LogError("No weather events have been added to the array.");
            return;
        
        }

        // Select a random event
        int randomIndex = Random.Range(0, weatherEvents.Length);
        WeatherEvent randomEvent = weatherEvents[randomIndex];
        
        StartCoroutine(ShowWeatherEvent(randomEvent));
    }

    // Coroutine to handle the display and duration of a weather event
    private IEnumerator ShowWeatherEvent(WeatherEvent weatherEvent)
    {
        Debug.Log("Weather Event: " + weatherEvent.name + " has started.");
        // Call StartTypingEffect with the weather event's name
        typingEffect.StartTypingEffect("Forecast: " + weatherEvent.name);
        yield return new WaitForSeconds(weatherEvent.duration);
        Debug.Log("Weather Event: " + weatherEvent.name + " has ended.");

        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the next event
        StartNextEvent(); // Automatically start the next event
    }
}