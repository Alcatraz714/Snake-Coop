using UnityEngine;
using System.Collections;
public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Shield, ScoreBoost, SpeedUp }
    public PowerUpType type;
    public float duration = 3f;

    public void Activate(Snake snake)
    {
        switch (type)
        {
            case PowerUpType.Shield:
                StartCoroutine(ActivateShield(snake));
                break;
            case PowerUpType.ScoreBoost:
                StartCoroutine(ActivateScoreBoost(snake));
                break;
            case PowerUpType.SpeedUp:
                StartCoroutine(ActivateSpeedUp(snake));
                break;
        }

        Destroy(gameObject);
    }

    private IEnumerator ActivateShield(Snake snake)
    {
        snake.hasShield = true;
        yield return new WaitForSeconds(duration);
        snake.hasShield = false;
    }

    private IEnumerator ActivateScoreBoost(Snake snake)
    {
        int originalScore = GameManager.Instance.scorePlayer1; // Assumes player 1
        GameManager.Instance.UpdateScore(snake.playerNumber, originalScore * 2);
        yield return new WaitForSeconds(duration);
    }

    private IEnumerator ActivateSpeedUp(Snake snake)
    {
        float originalSpeed = snake.moveSpeed;
        snake.moveSpeed *= 1.5f;
        yield return new WaitForSeconds(duration);
        snake.moveSpeed = originalSpeed;
    }
}
