using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    private int playerScore;
    private int pointsForDifficultyUp = 10;

    public TextMeshProUGUI currrentScoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    public AudioSource scoreUpAudio;
    public AudioSource windAudio;

    public GameObject gameOverMenu;
    public bool gameOver = false;

    public float pipeHeightOffset = 2;
    public float pipeMoveSpeed = 5;
    public float pipeDeadZone = -12;
    public float pipeDistance = 8;

    public float birdFlapStrength = 5.5f;

    public void IncreaseScore(int scoreToAdd)
    {
        SetScore(playerScore + scoreToAdd);
        scoreUpAudio.Play();
    }

    private void SetScore(int value)
    {
        playerScore = value;
        SetScoreText();

        if (playerScore % pointsForDifficultyUp == 0)
        {
            DifficultyUp();
        }

        windAudio.volume += 0.075f / pointsForDifficultyUp;
    }

    private void SetScoreText()
    {
        currrentScoreText.SetText(playerScore.ToString());
        finalScoreText.SetText("Final Score: " + playerScore.ToString());
    }

    public void GameOver()
    {
        gameOver = true;

        int highScore = PlayerPrefs.GetInt("highScore");

        if (playerScore > highScore)
        {
            EditHighScore();
        } else
        {
            highScoreText.color = Color.white;
            highScoreText.SetText("High Score: " + highScore);
        }

        windAudio.volume = 0.1f;
        pipeMoveSpeed = 0;
        gameOverMenu.SetActive(true);

    }

    private void EditHighScore()
    {
        PlayerPrefs.SetInt("highScore", playerScore);
        highScoreText.SetText("New High Score: " + playerScore + "!");

        VertexGradient rainbowGradient = new(Color.red, Color.white, Color.red, Color.white);
        highScoreText.colorGradient = rainbowGradient;
    }

    private void DifficultyUp()
    {
        pipeMoveSpeed += 0.5f;
    }
}
