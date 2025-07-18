using UnityEngine;

public class coinSpawner : MonoBehaviour
{
    public GameObject[] coinPrefab;

    public float spawnRate = 1.5f;
    
    public float spawnDistance = 50f;
    
    public float spawnWidth = 60f;
    
    public float spawnHeight = 60f;
    
    public float destroyDistance = 30f;

    private Transform player;           // Cached player reference
    private float nextSpawnTime;        // Timer for next spawn
    private bool canSpawn = true;       // Master spawn control switch

    void Start()
    {
        // Cache player transform at start for efficiency
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Early exit if spawning disabled or player missing
        if (!canSpawn || player == null) return;
        
        // Time-based spawning using Unity's unscaled time
        if (Time.time >= nextSpawnTime)
        {
            SpawnCoins();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
        
        // Cleanup coins behind player
        DestroyOffscreenCoins();
    }

    /// Globally stops all coin spawning (called on game end)
 
    public void StopSpawning() => canSpawn = false;

    void SpawnCoins()
    {
        // Calculate random position in front of player
        Vector3 spawnPos = new Vector3(
            player.position.x + spawnDistance, // Fixed X offset
            Random.Range(-spawnWidth, spawnWidth), // Random Y
            Random.Range(-spawnHeight, spawnHeight) // Random Z
        );

        // Instantiate random coin from prefab array
        GameObject newCoin = Instantiate(
            coinPrefab[Random.Range(0, coinPrefab.Length)],
            spawnPos,
            Quaternion.identity
        );

        // Remove physics if present (coins use custom movement)
        if (newCoin.GetComponent<Rigidbody>() != null)
        {
            Destroy(newCoin.GetComponent<Rigidbody>());
        }
    }

    void DestroyOffscreenCoins()
    {
        // Find all active coins in scene
        GameObject[] allCoins = GameObject.FindGameObjectsWithTag("Coin");
        
        foreach (GameObject coin in allCoins)
        {
            // Safety check + position validation
            if (coin != null && coin.transform.position.x < player.position.x - destroyDistance)
            {
                Destroy(coin);
            }
        }
    }
}