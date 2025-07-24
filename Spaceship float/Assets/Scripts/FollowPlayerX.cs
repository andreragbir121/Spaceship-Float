using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(5, 1, 42);
    private Transform _target;

    void LateUpdate()
    {
        if (_target == null)
        {
            // Auto-find player if target is missing
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null) _target = player.transform;
            else return;
        }
        
        transform.position = _target.position + offset;
    }

    // Call this when a new player spawns
    public void SetNewTarget(GameObject newTarget)
    {
        if (newTarget != null)
        {
            _target = newTarget.transform;
            Debug.Log($"Camera now following: {newTarget.name}");
        }
    }
}