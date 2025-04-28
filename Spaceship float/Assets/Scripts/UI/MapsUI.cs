using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapsUI : MonoBehaviour
{
    public Button[] mapButtons; // Reference to your map buttons
    public Text infoText; // Display info about map ownership

    // Assume you have a list of purchased maps (you can replace this with your own logic)
    private bool[] purchasedMaps = new bool[3]; // Example: 3 maps

    private void Start()
    {
        // Initialize map buttons (e.g., set interactable based on ownership)
        for (int i = 0; i < mapButtons.Length; i++)
        {
            mapButtons[i].interactable = purchasedMaps[i];
        }
    }

    // Called when a map button is clicked
    public void SelectMap(int mapIndex)
    {
        if (purchasedMaps[mapIndex])
        {
            // Load the main game scene with the selected map
            // You can use SceneManager.LoadScene or your own scene management logic
            // Set the selected map as a parameter to the main game scene
            // Example: SceneManager.LoadScene("MainGameScene", LoadSceneMode.Single);
        }
        else
        {
            infoText.text = "This map is not yet purchased!";
        }
    }
}

