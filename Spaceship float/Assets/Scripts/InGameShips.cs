using UnityEngine;

public class InGameShips : MonoBehaviour
{
    [SerializeField] private GameObject[] shipPrefabs;
    [SerializeField] private Transform shipSpawnPoint;

    private void Start()
    {
        int selectedShipIndex = PlayerPrefs.GetInt("SelectedShip", 0);
        
        if (selectedShipIndex < 0 || selectedShipIndex >= shipPrefabs.Length)
        {
            Debug.LogWarning("Invalid ship index, defaulting to 0");
            selectedShipIndex = 0;
        }

        if (shipPrefabs[selectedShipIndex] != null && shipSpawnPoint != null)
        {
            GameObject playerShip = Instantiate(
                shipPrefabs[selectedShipIndex],
                shipSpawnPoint.position,
                shipSpawnPoint.rotation
            );
            
            // Ensure the clone has the Player tag
            playerShip.tag = "Player";
            
            // Notify camera to follow this new ship
            FindObjectOfType<FollowPlayerX>()?.SetNewTarget(playerShip);
            
            Debug.Log($"Loaded ship {selectedShipIndex}");
        }
        else
        {
            Debug.LogError("Ship prefab or spawn point not assigned!");
        }
    }
}