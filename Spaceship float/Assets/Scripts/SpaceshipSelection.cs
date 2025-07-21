using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceshipSelection : MonoBehaviour
{
    [System.Serializable]
    public class Spaceship
    {
        public string shipName;
        public GameObject shipPrefab;
        public Sprite shipIcon;
        public int unlockCost = 0; // Set to 0 for testing
        [HideInInspector] public bool isUnlocked = true; // All unlocked for now
    }

    [Header("UI Elements")]
    [SerializeField] private Image shipDisplayImage;
    [SerializeField] private Text shipNameText;
    [SerializeField] private Text shipCostText;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;

    [Header("Spaceships")]
    [SerializeField] private Spaceship[] ships;
    
    private int currentIndex = 0;
    private GameObject currentShipModel;

    void Start()
    {
        // Initialize UI
        UpdateShipDisplay();
        
        // Button listeners
        nextButton.onClick.AddListener(NextShip);
        prevButton.onClick.AddListener(PreviousShip);
        selectButton.onClick.AddListener(SelectShip);
    }

    void UpdateShipDisplay()
    {
        // Update UI elements
        shipNameText.text = ships[currentIndex].shipName;
        shipDisplayImage.sprite = ships[currentIndex].shipIcon;
        shipCostText.text = ships[currentIndex].isUnlocked ? "UNLOCKED" : $"COST: {ships[currentIndex].unlockCost}";
        
        // Enable/disable select button based on unlock status
        selectButton.interactable = ships[currentIndex].isUnlocked;
        
        // Update 3D model preview (if using)
        if (currentShipModel != null) Destroy(currentShipModel);
        currentShipModel = Instantiate(ships[currentIndex].shipPrefab, Vector3.zero, Quaternion.identity);
    }

    public void NextShip()
    {
        currentIndex = (currentIndex + 1) % ships.Length;
        UpdateShipDisplay();
    }

    public void PreviousShip()
    {
        currentIndex = (currentIndex - 1 + ships.Length) % ships.Length;
        UpdateShipDisplay();
    }

    public void SelectShip()
    {
        // Save selected ship (you'll need a GameManager or similar)
        PlayerPrefs.SetInt("SelectedShip", currentIndex);
        Debug.Log($"Selected ship: {ships[currentIndex].shipName}");
        
        // Load game scene
        SceneManager.LoadScene("GameScene");
    }

    // Call this when unlock system is implemented
    public void UnlockCurrentShip()
    {
        if (CurrencyManager.Instance.SpendCoins(ships[currentIndex].unlockCost))
        {
            ships[currentIndex].isUnlocked = true;
            UpdateShipDisplay();
        }
    }
}