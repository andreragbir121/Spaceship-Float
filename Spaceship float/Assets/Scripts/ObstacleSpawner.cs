using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Array of asteroid prefabs to randomly choose from
    public GameObject[] asteroidPrefabs;
    
    // How frequently asteroids spawn (per second)
    public float spawnRate = 1.5f;

    // Distance in front of player to spawn asteroids
    public float spawnDistance = 50f;
    
    // Vertical spawn area range
    public float spawnWidth = 60f;
    
    // Depth spawn area range
    public float spawnHeight = 60f;
    
    // Distance behind player where asteroids get destroyed
    public float destroyDistance = 30f;

    // Minimum and maximum size for spawned asteroids
    public float minAsteroidSize = 1f;
    public float maxAsteroidSize = 20f;

    // Reference to player's transform
    private Transform player;
    
    // Timer tracking next spawn time
    private float nextSpawnTime;
    
    // Flag to control spawning
    private bool canSpawn = true;

    void Start()
    {
        // Get player reference at start
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Skip if spawning disabled or player missing
        if (!canSpawn || player == null) return;
        
        // Check if time to spawn new asteroid
        if (Time.time >= nextSpawnTime)
        {
            SpawnAsteroid();
            // Calculate next spawn time
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
        
        // Remove asteroids behind player
        DestroyOffscreenAsteroids();
    }

    // Disable further spawning
    public void StopGame() => canSpawn = false;

    void OnEnable()
    {
        // Reset spawn timer
        nextSpawnTime = 0f;
        // Re-enable spawning
        canSpawn = true;

        // Refresh player reference
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) Debug.LogError("Player not found in scene!");
    }

    void SpawnAsteroid()
    {
        // Calculate random spawn position in front of player
        Vector3 spawnPos = player.position +
                         new Vector3(
                             spawnDistance,
                             Random.Range(-spawnWidth, spawnWidth),
                             Random.Range(-spawnHeight, spawnHeight));

        // Create asteroid from random prefab
        GameObject newAsteroid = Instantiate(
           asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)],
           spawnPos,
           Quaternion.identity
       );

        // Set random size
        float randomScale = Random.Range(minAsteroidSize, maxAsteroidSize);
        newAsteroid.transform.localScale = Vector3.one * randomScale;
    }

    void DestroyOffscreenAsteroids()
    {
        // Get all active asteroids
        GameObject[] allAsteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        
        foreach (GameObject asteroid in allAsteroids)
        {
            // Destroy if too far behind player
            if (asteroid.transform.position.x < player.position.x - destroyDistance)
            {
                Destroy(asteroid);
            }
        }
    }
}