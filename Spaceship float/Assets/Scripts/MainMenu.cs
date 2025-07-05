using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Assign in Inspector
    public GameObject mainPanel;    // Main menu with Start button
    public GameObject mapsPanel;    // Map selection UI
    public GameObject shipsPanel;   // Ship selection UI

    private int selectedMapIndex;   // Track chosen map

    void Start()
    {
        // Start with main menu visible
        ReturnToMainMenu();
    }

    // ====== MAIN MENU FLOW ======
    public void OnStartClicked()
    {
        // Hide main menu, show map selection
        mainPanel.SetActive(false);
        mapsPanel.SetActive(true);
    }

    // ====== MAP SELECTION ======
    public void SelectMap(int mapIndex)
    {
        PlayerPrefs.SetInt("SelectedMap", mapIndex);
        StartGame();
    }

    // ====== SHIP SELECTION ======
    public void ShowShipSelection()
    {
        mainPanel.SetActive(false);
        mapsPanel.SetActive(false);
        shipsPanel.SetActive(true);
    }

    public void SelectShip(int shipIndex)
    {
        PlayerPrefs.SetInt("SelectedShip", shipIndex);
        StartGame(); // Load game after both selections
    }

    // ====== NAVIGATION ======
    public void ReturnToMainMenu()
    {
        mainPanel.SetActive(true);
        mapsPanel.SetActive(false);
        shipsPanel.SetActive(false);
    }

    // ====== CORE FUNCTIONS ======
    public void StartGame()
    {
        SceneManager.LoadScene(selectedMapIndex + 1); // Scene 0=menu, 1+=maps
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
