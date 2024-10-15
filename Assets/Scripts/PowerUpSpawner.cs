using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;
    public float spawnInterval = 10f;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    private float timeSinceLastSpawn; // time after which the poweup is destroyed we dont want the screen to be full

    void Start()
    {
        SpawnPowerUp();
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        // Spawn power-up after the interval has passed
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnPowerUp();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnPowerUp()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        // Random poweup to spawn (3 - prsent in the game)
        int randomPowerUpIndex = Random.Range(0, powerUpPrefabs.Length);
        Instantiate(powerUpPrefabs[randomPowerUpIndex], spawnPosition, Quaternion.identity);
    }
}

