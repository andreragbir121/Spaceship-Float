using UnityEngine;

public class coin : MonoBehaviour
{
    public int value = 1; // Set this in Inspector per coin type
    public float moveSpeed = 5f; 
    private Transform player;

    private Transform _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<Collider>().isTrigger = true; // Auto-set trigger
    }

    void Update()
    {
        // Move toward player (X-axis only)
        Vector3 direction = (_player.position - transform.position).normalized;
        direction.y = 0;

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            Collect();
        }
    }
    // Called when player collects the coin
    public void Collect()
    {
        // Add to player's currency (replace with your system)
        CurrencyManager.Instance.AddCoins(value);

        // Play effects (sound/particles) here if needed
        Destroy(gameObject);
    }
}