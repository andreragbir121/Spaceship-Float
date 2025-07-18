using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerX : MonoBehaviour
{
    // Movement and rotation settings
    public float speed = 10f;          // Forward movement speed
    public float rotationSpeed = 5f;   // Tilt rotation speed
    public float maxTiltAngle = 45f;   // Maximum tilt angle
    
    // Input and effects
    private bool isHolding = false;    // Track if input is being held
    public GameObject explosionEffect; // Death explosion prefab
    public ParticleSystem engineFlame; // Engine visual effect

    void FixedUpdate()
    {
        // Constant forward movement
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void Update()
    {
        // Handle mouse/touch input
        if (Input.GetMouseButtonDown(0)) isHolding = true;
        if (Input.GetMouseButtonUp(0)) isHolding = false;

        // Get current rotation and normalize angle
        Vector3 currentRotation = transform.rotation.eulerAngles;
        if (currentRotation.x > 180) currentRotation.x -= 360;

        // Calculate target tilt based on input
        float targetRotationX = 0f;
        if (isHolding)
        {
            // Tilt up when holding input
            targetRotationX = Mathf.Clamp(currentRotation.x - rotationSpeed * Time.deltaTime, -maxTiltAngle, maxTiltAngle);
        }
        else
        {
            // Tilt down when not holding
            targetRotationX = Mathf.Clamp(currentRotation.x + rotationSpeed * Time.deltaTime, -maxTiltAngle, maxTiltAngle);
        }

        // Apply new rotation
        transform.rotation = Quaternion.Euler(targetRotationX, currentRotation.y, currentRotation.z);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check for asteroid collision
        if (other.CompareTag("Asteroid"))
        {
            // Stop game systems
            FindObjectOfType<coinSpawner>()?.StopSpawning();
            FindObjectOfType<GameManager>()?.StopGame();
            FindObjectOfType<scoreUI>()?.SaveTotalCoins();

            // Show explosion effect
            if (explosionEffect != null)
                Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Destroy player and reload scene
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }

    void OnDestroy()
    {
        // Safety cleanup on destruction
        FindObjectOfType<GameManager>()?.StopGame();
        FindObjectOfType<coinSpawner>()?.StopSpawning();
        isHolding = false; // Reset input state
    }
}