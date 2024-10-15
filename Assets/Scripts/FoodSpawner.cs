using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject[] foodPrefabs; // Mass Gainer, Mass Burner
    public float spawnInterval = 5f;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnFood), spawnInterval, spawnInterval);
    }

    void SpawnFood()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        int randomFoodIndex = Random.Range(0, foodPrefabs.Length);
        Instantiate(foodPrefabs[randomFoodIndex], spawnPosition, Quaternion.identity);
    }
}
