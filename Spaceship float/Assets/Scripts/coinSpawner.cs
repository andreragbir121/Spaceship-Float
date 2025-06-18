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
        DestroyOffscreenCoins();
    }

    // Creates a new asteroid
    void SpawnCoins()
    {
        // Calculate spawn position relative to world space (not player)
        Vector3 spawnPos = new Vector3(
            player.position.x + spawnDistance, // Fixed X position
            Random.Range(-spawnWidth, spawnWidth),
            Random.Range(-spawnHeight, spawnHeight)
        );

        // Create new coin (no movement component needed)
        GameObject newCoin = Instantiate(
            coinPrefab[Random.Range(0, coinPrefab.Length)],
            spawnPos,
            Quaternion.identity
        );

        // Ensure coin has no movement scripts attached
        if (newCoin.GetComponent<Rigidbody>() != null)
        {
            Destroy(newCoin.GetComponent<Rigidbody>());
        }
    }

    // Removes coins that are behind the player
    void DestroyOffscreenCoins()
    {
        GameObject[] allCoins = GameObject.FindGameObjectsWithTag("Coin");
        
        foreach (GameObject coin in allCoins)
        {
            if (coin != null && coin.transform.position.x < player.position.x - destroyDistance)
            {
                Destroy(coin);
            }
        }
    }
}