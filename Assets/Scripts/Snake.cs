using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public List<Transform> tail = new List<Transform>();  // List of tail segments
    public GameObject tailPrefab;  // Prefab for tail segments
    public Transform tailAttachPoint;  // Empty child object behind the head
    public float moveSpeed = 5f;
    public bool hasShield = false;
    public int playerNumber = 1;

    private bool growTail = false;
    private bool shrinkTail = false;

    private void Update()
    {
        HandleInput();
        Move();
        ScreenWrap();
    }

    void HandleInput()
    {
        if (playerNumber == 1)
        {
            if (Input.GetKeyDown(KeyCode.W)) direction = Vector2.up;
            if (Input.GetKeyDown(KeyCode.S)) direction = Vector2.down;
            if (Input.GetKeyDown(KeyCode.A)) direction = Vector2.left;
            if (Input.GetKeyDown(KeyCode.D)) direction = Vector2.right;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) direction = Vector2.up;
            if (Input.GetKeyDown(KeyCode.DownArrow)) direction = Vector2.down;
            if (Input.GetKeyDown(KeyCode.LeftArrow)) direction = Vector2.left;
            if (Input.GetKeyDown(KeyCode.RightArrow)) direction = Vector2.right;
        }
    }

    void Move()
    {
        Vector2 currentPos = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, moveSpeed * Time.deltaTime);

        // Handle tail growing
        if (growTail)
        {
            Vector2 tailPosition;

            // If there are tail segments, spawn the new tail at the last segment's position
            if (tail.Count > 0)
            {
                tailPosition = tail[tail.Count - 1].position;
            }
            else
            {
                // Use the tailAttachPoint position for the first tail piece
                tailPosition = tailAttachPoint.position;
            }

            // Add the next piece when pick up the food
            GameObject newTail = Instantiate(tailPrefab, tailPosition, Quaternion.identity);
            tail.Add(newTail.transform);

            growTail = false;  // Reset the flag
        }

        // Move the tail segments
        if (tail.Count > 0)
        {
            for (int i = tail.Count - 1; i > 0; i--)
            {
                tail[i].position = tail[i - 1].position;
            }
            tail[0].position = currentPos;
        }

        // Handle tail shrinking
        if (shrinkTail && tail.Count > 0)
        {
            Destroy(tail[tail.Count - 1].gameObject);
            tail.RemoveAt(tail.Count - 1);
            shrinkTail = false;
        }
    }

    void ScreenWrap()
    {
        // Screen wrapping logic
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0) pos.x = 1;
        if (pos.x > 1) pos.x = 0;
        if (pos.y < 0) pos.y = 1;
        if (pos.y > 1) pos.y = 0;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Food food = collision.GetComponent<Food>();

            if (food.type == FoodType.MassGainer)
            {
                growTail = true;  // Grow the tail
                GameManager.Instance.UpdateScore(playerNumber, 10);  // Increase score by 10
            }
            else if (food.type == FoodType.MassBurner && tail.Count > 0)
            {
                shrinkTail = true;  // Shrink the tail
                GameManager.Instance.UpdateScore(playerNumber, -10);  // Decrease score by 10
            }

            Destroy(collision.gameObject);  // Destroy the food
        }
        else if (collision.CompareTag("PowerUp"))
        {
            PowerUp powerUp = collision.GetComponent<PowerUp>();
            powerUp.Activate(this);  // Activate the power-up
        }
        else if (collision.CompareTag("Snake") && !hasShield)
        {
            Snake otherSnake = collision.GetComponent<Snake>();
            if (otherSnake != null && otherSnake.playerNumber != playerNumber && !hasShield && !otherSnake.hasShield)
            {
                Debug.Log("Game Over: Player " + playerNumber + " and Player " + otherSnake.playerNumber);
                GameManager.Instance.GameOver(playerNumber);
                GameManager.Instance.GameOver(otherSnake.playerNumber);
            }
        }
    }
}
