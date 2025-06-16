using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is targetted to the obstacles. where obstacles move directly towards the player to destroy the player object
/// </summary>

public class ObstacleFollowPlayer : MonoBehaviour
{
    public float forwardSpeed;
    public float minRotationSpeed = 20f;  // Minimum rotation speed (degrees/sec)
    public float maxRotationSpeed = 100f; // Maximum rotation speed (degrees/sec)
    
    private Vector3 rotationAxis;        // Random rotation axis
    private float rotationSpeed;         // Random speed
    // Start is called before the first frame update
    void Start()
    {
      // Set random rotation axis and speed
        rotationAxis = Random.onUnitSphere; // Random 3D direction
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

    }

    // Update is called once per frame
    void Update()
    {
    }


    void FixedUpdate()
    {
        transform.Translate(Vector3.right * forwardSpeed * Time.deltaTime);
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);

    }
    
}
