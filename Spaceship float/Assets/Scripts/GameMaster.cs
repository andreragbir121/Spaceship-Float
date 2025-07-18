using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

       void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene changes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    // Callback when new scene finishes loading
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset game state when returning to main menu (scene 0)
        if (scene.buildIndex == 0)
        {
            ResetGameState();
        }
    }
    // Resets all game systems to initial state
    public void ResetGameState()
    {
        // Clean up all persistent objects
        CleanPersistentObjects();

        // Reset critical systems
        ResetCoreSystems();

        // Prepare for new session
        Time.timeScale = 1f; // Ensure game isn't paused
    }

    private void CleanPersistentObjects()
    {
        // Find all objects in the DontDestroyOnLoad scene (buildIndex -1)
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (var obj in allObjects)
        {
            if (obj.scene.buildIndex == -1 && obj != this.gameObject)
            {
                Destroy(obj);
            }
        }
    }

    private void ResetCoreSystems()
    {
        // Clean up known manager types
        DestroyIfExists<CurrencyManager>();
        DestroyIfExists<GameManager>();
        DestroyIfExists<coinSpawner>();

        /* Note: For production use, consider:
        1. Maintaining a list of system types to reset
        2. Using interfaces for resetable systems
        3. Object pooling instead of destruction */
    }

    private void DestroyIfExists<T>() where T : MonoBehaviour
    {
        T instance = FindObjectOfType<T>();
        if (instance != null && instance.gameObject != this.gameObject)
        {
            Destroy(instance.gameObject);
        }
    }

    // Call this to manually trigger full game reset
    public void ForceGameReset()
    {
        ResetGameState();
        SceneManager.LoadScene(0);
    }
}