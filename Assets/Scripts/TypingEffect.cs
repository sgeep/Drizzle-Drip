using System.Collections;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public float typingSpeed = 0.05f;
    private bool isTyping = false;
    private bool isBlinking = false;
    private Coroutine blinkingCoroutine;

    private void Awake()
    {
        // Start with the cursor blinking
        StartBlinkingCursor();
    }

    public void StartTypingEffect(string newText)
    {
        if (isTyping)
        {
            StopCoroutine(nameof(TypeText)); // Stop the current typing if it's already happening
        }
        if (isBlinking)
        {
            StopBlinkingCursor(); // Stop the cursor from blinking while typing
        }
        StartCoroutine(TypeText(newText));
    }

    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        textDisplay.text = ""; // Clear existing text

        foreach (char letter in textToType.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        StartBlinkingCursor(); // Resume cursor blinking after typing is finished
    }

    void StartBlinkingCursor()
    {
        if (!isBlinking)
        {
            blinkingCoroutine = StartCoroutine(BlinkingCursor());
        }
    }

    void StopBlinkingCursor()
    {
        if (blinkingCoroutine != null)
        {
            StopCoroutine(blinkingCoroutine);
            isBlinking = false;
            // Ensure cursor is not visible after stopping
            if (textDisplay.text.EndsWith("|"))
            {
                textDisplay.text = textDisplay.text.Substring(0, textDisplay.text.Length - 1);
            }
        }
    }

    IEnumerator BlinkingCursor()
    {
        isBlinking = true;
        while (true)
        {
            yield return new WaitForSeconds(0.5f); // Wait half a second
            textDisplay.text += "|"; // Show cursor
            yield return new WaitForSeconds(0.5f); // Wait half a second
            if (textDisplay.text.EndsWith("|"))
            {
                textDisplay.text = textDisplay.text.Substring(0, textDisplay.text.Length - 1); // Hide cursor
            }
        }
    }
}
