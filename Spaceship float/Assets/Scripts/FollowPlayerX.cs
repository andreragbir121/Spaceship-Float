using UnityEngine;
using System.Collections;   
public class FollowPlayerX : MonoBehaviour
{
    // ========== CONFIGURATION ==========
    public GameObject plane;
    
    public Vector3 offset = new Vector3(5, 1, 42);

    private Transform _planeTransform;  // Cached transform for efficiency
    private bool _isValid = true;      // Safety flag for target validity

    void Start()
    {
        // Null check for required reference
        if (plane == null)
        {
            Debug.LogError("Plane reference not set in FollowPlayerX!", this);
            _isValid = false;
            return;
        }

        // Cache transform for performance
        _planeTransform = plane.transform;
    }

    // ========== MAIN UPDATE LOOP ==========
    void Update()
    {
        // Early exit if target is invalid/missing
        if (!_isValid || _planeTransform == null)
        {
            /* Potential extension:
            if (autoRecover) {
                TryFindNewTarget();
            }*/
            return;
        }

        // Maintain consistent offset from target
        transform.position = _planeTransform.position + offset;
    }

    // Call this when target is being destroyed to prevent errors
  
    public void OnTargetDestroyed()
    {
        _isValid = false;
        _planeTransform = null;
        
        /* Optional extensions:
        - Switch to alternate target
        - Enable cinematic camera mode
        - Trigger game over sequence */
    }

    /* Optional recovery method
    private void TryFindNewTarget()
    {
        GameObject newTarget = GameObject.FindGameObjectWithTag("Player");
        if (newTarget != null) {
            plane = newTarget;
            _planeTransform = newTarget.transform;
            _isValid = true;
        }
    }*/
}