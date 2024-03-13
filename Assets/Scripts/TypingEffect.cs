using System.Collections;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    // References to the TextMeshPro UI elements for intro and weather texts.
    public TextMeshProUGUI introTextDisplay;
    public TextMeshProUGUI weatherTextDisplay;

    // Typing speed in seconds, customizable in the Unity Editor.
    public float typingSpeed = 0.05f;

    // Extended delay for the "..." in the intro text.
    public float extendedDelay = 0.75f;

    // Reference to the WeatherController to notify when intro is complete.
    private WeatherController weatherController;

    private void Awake()
    {
        // Attempt to automatically find the WeatherController in the scene.
        weatherController = FindObjectOfType<WeatherController>();
        if (weatherController == null)
        {
            Debug.LogError("WeatherController not found in the scene.");
        }
    }

    public void StartIntroSequence()
    {
        // Clears any previous text and starts typing the intro text.
        StartCoroutine(TypeIntroText("Today's weather looks"));
    }

    IEnumerator TypeIntroText(string textToType)
    {
        introTextDisplay.text = ""; // Ensure the text starts empty.

        // Typing out each character in the intro text.
        foreach (char letter in textToType.ToCharArray())
        {
            introTextDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Adding extended delay dots at the end of the intro.
        for (int i = 0; i < 3; i++)
        {
            introTextDisplay.text += ".";
            yield return new WaitForSeconds(extendedDelay);
        }

        // Notify the WeatherController that the intro is complete.
        weatherController.OnIntroComplete();
    }

    public void StartWeatherEventTyping(string eventName)
    {
        StopAllCoroutines(); // Stop any existing typing coroutines
        StartCoroutine(TypeText(eventName, weatherTextDisplay)); // Starts typing the weather event name after the intro.
    }

    IEnumerator TypeText(string textToType, TextMeshProUGUI textDisplay)
    {
        textDisplay.text = ""; // Start with an empty text.

        // Type out each character of the given text.
        foreach (char letter in textToType.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}