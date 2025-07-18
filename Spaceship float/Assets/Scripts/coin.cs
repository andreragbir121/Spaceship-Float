using UnityEngine;

public class coin : MonoBehaviour
{
    [Tooltip("How many coins this is worth when collected")]
    public int value = 1;
    
    [Tooltip("How fast the coin moves toward player (units/second)")]
    public float moveSpeed = 5f;
    
    [Tooltip("Visual effect when collected (optional)")]
    public GameObject explosionEffect;

    private Transform _player;      // Cached player transform reference
    private bool _isCollected;      // Flag to prevent double-collection

    void Start()
    {
        // Safely find and cache player reference
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Coin will not move.");
            enabled = false; // Disable entire script if no player exists
        }

        // Ensure this coin can be triggered
        GetComponent<Collider>().isTrigger = true;
    }

    void Update()
    {
        // Early exit if conditions aren't met
        if (_player == null || _isCollected) return;

        // Calculate movement toward player (X-axis only)
        Vector3 direction = (_player.position - transform.position).normalized;
        direction.y = 0; // Zero out vertical movement
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Only proceed if:
        // 1. Not already collected
        // 2. Colliding with player
        if (_isCollected || !other.CompareTag("Player")) return;

        _isCollected = true; // Set flag first to prevent re-triggering
        Collect();
    }

    void Collect()
    {
        // Play visual effect if assigned
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Add value to currency system if available
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddCoins(value);
        }
        else
        {
            Debug.LogWarning("CurrencyManager not found! Coin value lost.");
        }

        // Remove coin from game
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        // Cleanup any pending operations
        CancelInvoke();         // Stops all scheduled method calls
        StopAllCoroutines();    // Halts any running coroutines
    }
}