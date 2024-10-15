using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject GameOverScreen;

    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateScore(int player, int amount)
    {
        if (player == 1) scorePlayer1 += amount;
        else scorePlayer2 += amount;
    }

    public void GameOver(int player)
    {
        Debug.Log("Game over");
        GameOverScreen.SetActive(true);
    }
}
