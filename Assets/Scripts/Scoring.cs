using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign this in the Inspector
    private int score = 0;
    private bool hasScoredThisRound = false;

    private void Start()
    {
        UpdateScoreText();
    }

    public void CorrectGuess()
    {
        if (!hasScoredThisRound)
        {
            score++;
            hasScoredThisRound = true;
            UpdateScoreText();
            Debug.Log("Correct! Score: " + score);
        }
    }

    public void IncorrectGuess()
    {
        ResetScore();
        Debug.Log("Incorrect. Score reset to 0.");
    }

    public void ResetScore()
    {
        score = 0;
        hasScoredThisRound = false;
        UpdateScoreText();
    }

    public void NewRound()
    {
        hasScoredThisRound = false;
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}