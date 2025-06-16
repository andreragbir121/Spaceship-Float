using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed = 10f; // Forward movement speed
    public float rotationSpeed = 5f; // Speed of tilting
    public float maxTiltAngle = 45f; // Maximum tilt angle in degrees
    private bool isHolding = false; // Check if the button is being held
    public GameObject explosionEffect;
    void FixedUpdate()
    {
        // Move the plane forward at a constant rate
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void Update()
    {
        // Check for mouse button or touch input
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button or a single touch
        {
            isHolding = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isHolding = false;
        }

        // Get the current rotation in Euler angles
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Convert current rotation to a -180 to 180 range for easier calculations
        if (currentRotation.x > 180)
        {
            currentRotation.x -= 360;
        }

        // Calculate the target tilt angle based on input
        float targetRotationX = 0f; // Default target angle (no tilt)
        if (isHolding)
        {
            // Tilt upward
            targetRotationX = Mathf.Clamp(currentRotation.x - rotationSpeed * Time.deltaTime, -maxTiltAngle, maxTiltAngle);
        }
        else
        {
            // Tilt downward
            targetRotationX = Mathf.Clamp(currentRotation.x + rotationSpeed * Time.deltaTime, -maxTiltAngle, maxTiltAngle);
        }

        // Apply the new rotation while keeping the Y and Z rotations unchanged
        transform.rotation = Quaternion.Euler(targetRotationX, currentRotation.y, currentRotation.z);
    }

 void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            if (explosionEffect != null) 
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
            Debug.Log("Player destroyed!");
        }
    }
}