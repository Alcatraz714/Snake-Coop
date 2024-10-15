using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public GameObject gameOverScreen;
    public Button restartButton;
    public Button quitButton;

    private void Update()
    {
        player1ScoreText.text = "Player 1: " + GameManager.Instance.scorePlayer1;
        player2ScoreText.text = "Player 2: " + GameManager.Instance.scorePlayer2;
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }
}
