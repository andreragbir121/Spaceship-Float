using UnityEngine;
using System.Collections.Generic;

public class coinSpawner : MonoBehaviour
{
     // Asteroid prefabs to spawn (set these in Unity Inspector)
    public GameObject[] coinPrefab;
    
    // How many asteroids spawn per second
    public float spawnRate = 1.5f;
    
    // How far ahead of player to spawn (X-axis)
    public float spawnDistance = 50f;
    
    // Vertical spread range (Y-axis)
    public float spawnWidth = 60f;
    
    // Depth spread range (Z-axis)
    public float spawnHeight = 60f;
    
    // How far behind player before destroying asteroids
    public float destroyDistance = 30f;

    // Reference to player's position
    private Transform player;
    
    // Timer for next spawn
    private float nextSpawnTime;

    public float minAsteroidSize = 1f;
    public float maxAsteroidSize = 1f;

    // Called when game starts
    void Start()
    {
        // Find player object by its "Player" tag and get its Transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Called every frame
    void Update()
    {
        // Check if it's time to spawn new asteroid
        if (Time.time >= nextSpawnTime)
        {
            SpawnCoins();
            // Set next spawn time based on spawnRate
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
        // Clean up old asteroids
        DestroyOffscreenCoins();
    }

    // Creates a new asteroid
    void SpawnCoins()
    {
        // Calculate spawn position:
        // - Always spawnDistance units ahead on X
        // - Random Y position within spawnWidth range
        // - Random Z position within spawnHeight range
        Vector3 spawnPos = player.position +
                         new Vector3(
                             spawnDistance,
                             Random.Range(-spawnWidth, spawnWidth),
                             Random.Range(-spawnHeight, spawnHeight));


        // Create new asteroid:
        // 1. Pick random prefab from array
        // 2. Set position
        // 3. No rotation (Quaternion.identity)
        GameObject newCoin = Instantiate(
           coinPrefab[Random.Range(0, coinPrefab.Length)],
           spawnPos,
           Quaternion.identity
       );

        // // Apply random uniform scale
        float randomScale = Random.Range(minAsteroidSize, maxAsteroidSize);
        newCoin.transform.localScale = Vector3.one * randomScale;
        
    }

    // Removes asteroids that are behind the player
    void DestroyOffscreenCoins()
    {
        // Get all objects tagged "Asteroid"
        GameObject[] allCoins = GameObject.FindGameObjectsWithTag("Coin");
        
        foreach (GameObject Coin in allCoins)
        {
            // If asteroid is destroyDistance units behind player on X-axis
            if (Coin.transform.position.x < player.position.x - destroyDistance)
            {
                // Remove it from the game
                Destroy(Coin);
            }
        }
    }
}