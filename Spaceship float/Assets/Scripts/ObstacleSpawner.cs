using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject[] asteroidPrefabs;
    public float spawnRate = 1f;
    public float spawnDistance = 50f; // Distance AHEAD on X-axis
    public float spawnWidth = 30f;    // Vertical (Y-axis) spread
    public float spawnHeight = 30f;   // Depth (Z-axis) spread
    public float destroyDistance = 30f; // Behind player

    private Transform player;
    private float nextSpawnTime;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnAsteroid();
            nextSpawnTime = Time.time + 1f/spawnRate;
        }
        DestroyOffscreenAsteroids();
    }

    void SpawnAsteroid()
    {
        // Spawn in front (X+), with vertical (Y) and depth (Z) randomness
        Vector3 spawnPos = player.position + 
                         new Vector3(
                             spawnDistance, // Always ahead on X
                             Random.Range(-spawnWidth, spawnWidth),
                             Random.Range(-spawnHeight, spawnHeight));

        Instantiate(
            asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)],
            spawnPos,
            Quaternion.identity
        );
    }

    void DestroyOffscreenAsteroids()
    {
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            // Destroy if too far behind on X-axis
            if (asteroid.transform.position.x < player.position.x - destroyDistance)
            {
                Destroy(asteroid);
            }
        }
    }

    // Visualize spawn area in Scene view
    void OnDrawGizmosSelected()
    {
        if (player == null) return;
        
        Gizmos.color = Color.green;
        Vector3 center = player.position + Vector3.right * spawnDistance;
        Vector3 size = new Vector3(5f, spawnWidth*2, spawnHeight*2);
        Gizmos.DrawWireCube(center, size);
    }
}