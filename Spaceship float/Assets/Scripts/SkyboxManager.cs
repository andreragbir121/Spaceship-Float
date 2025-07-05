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

    void Start()
    {
        int selectedMap = PlayerPrefs.GetInt("SelectedMap", 0);
        ApplySkybox(selectedMap);
    }

    void Update()
    {
        // Rotate based on selected map's speed
        float currentSpeed = maps[PlayerPrefs.GetInt("SelectedMap")].rotationSpeed;
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * currentSpeed);
    }

    void ApplySkybox(int mapIndex)
    {
        if (mapIndex >= maps.Length) return;
        RenderSettings.skybox = maps[mapIndex].skyboxMaterial;
    }
}