using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [System.Serializable]
    public class MapConfig
    {
        public string name;
        public Material skyboxMaterial;
        public float rotationSpeed = 1.5f;
    }

    public MapConfig[] maps; // Assign all 4 maps in Inspector

    private int currentMapIndex = 0;

    void Start()
    {
        currentMapIndex = PlayerPrefs.GetInt("SelectedMap", 0);
        ApplySkybox(currentMapIndex);
    }

    void Update()
    {
        // Skip if no skybox material is set
        if (RenderSettings.skybox == null) return;

        // Prevent array out of bounds
        if (currentMapIndex >= maps.Length || currentMapIndex < 0) return;

        // Skip if map config is null
        if (maps[currentMapIndex] == null) return;

        // Rotate based on selected map's speed
        float currentSpeed = maps[currentMapIndex].rotationSpeed;
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * currentSpeed);
    }

    void ApplySkybox(int mapIndex)
    {
        // Validate array bounds
        if (mapIndex >= maps.Length || mapIndex < 0) return;

        // Validate map config exists
        if (maps[mapIndex] == null)
        {
            Debug.LogError($"Map config at index {mapIndex} is null!");
            return;
        }

        // Validate material exists
        if (maps[mapIndex].skyboxMaterial == null)
        {
            Debug.LogError($"Skybox material for map {maps[mapIndex].name} is null!");
            return;
        }

        currentMapIndex = mapIndex;
        RenderSettings.skybox = maps[mapIndex].skyboxMaterial;
        DynamicGI.UpdateEnvironment();
    }

    void OnEnable() // Changed from Start
    {
        currentMapIndex = PlayerPrefs.GetInt("SelectedMap", 0);
        ApplySkybox(currentMapIndex);
    }
}
