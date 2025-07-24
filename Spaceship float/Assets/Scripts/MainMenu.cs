using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // UI Panels (assign in Inspector)
    public GameObject mainPanel;    // Main menu
    public GameObject mapsPanel;    // Map selection
    public GameObject shipsPanel;   // Ship selection

    // Selection tracking
    private int selectedMapIndex = 0;
    private bool cameFromStartFlow = false;

    void Start()
    {
        ReturnToMainMenu();
    }

    // ====== MAIN MENU BUTTONS ======
    public void OnStartClicked()
    {
        cameFromStartFlow = true;
        mainPanel.SetActive(false);
        mapsPanel.SetActive(true);
    }

    public void OnMapsClicked()
    {
        cameFromStartFlow = false;
        mainPanel.SetActive(false);
        mapsPanel.SetActive(true);
    }

    public void OnShipsClicked()
    {
        cameFromStartFlow = false;
        mainPanel.SetActive(false);
        shipsPanel.SetActive(true);
    }

    // ====== MAP SELECTION ======
    public void SelectMap(int mapIndex)
    {
        selectedMapIndex = mapIndex;
        PlayerPrefs.SetInt("SelectedMap", mapIndex);

        if (cameFromStartFlow)
        {
            // Continue to ship selection
            mapsPanel.SetActive(false);
            shipsPanel.SetActive(true);
        }
        else
        {
            // Just browsing maps - return to main menu
            ReturnToMainMenu();
        }
    }

    // ====== SHIP SELECTION ======
    public void SelectShip(int shipIndex)
    {
        PlayerPrefs.SetInt("SelectedShip", shipIndex);
        
        if (cameFromStartFlow)
        {
            // Start game immediately with selected map
            StartGame();
        }
        else
        {
            // Just browsing ships - start with default map
            selectedMapIndex = 0; // Or PlayerPrefs.GetInt("SelectedMap", 0);
            StartGame();
        }
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