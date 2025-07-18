using UnityEngine;
using UnityEngine.UI;

public class scoreUI : MonoBehaviour
{
    [SerializeField] private Text _currentCoinsText;
    
    [SerializeField] private Text _totalCoinsText;


    [SerializeField] private int _totalCoinsEverCollected = 0;

    private int _currentSessionCoins = 0; // Coins collected in current play session

    void Start()
    {
        // Initialize UI displays
        UpdateCurrentCoinsDisplay(0);
        UpdateTotalCoinsDisplay();
        
        // Subscribe to currency updates
        CurrencyManager.OnCoinsUpdated += HandleCoinChange;
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        CurrencyManager.OnCoinsUpdated -= HandleCoinChange;
    }

    /// Handles changes to the player's coin count
  
    private void HandleCoinChange(int newAmount)
    {
        // Calculate difference from previous amount
        int delta = newAmount - _currentSessionCoins;
        _currentSessionCoins = newAmount;
        _totalCoinsEverCollected += delta;

        // Update both displays
        UpdateCurrentCoinsDisplay(newAmount);
        UpdateTotalCoinsDisplay();
    }

    // Updates current session coin display
    private void UpdateCurrentCoinsDisplay(int amount)
    {
        if (_currentCoinsText != null)
            _currentCoinsText.text = $"Coins: {amount}";
    }

    // Updates lifetime total coin display
    private void UpdateTotalCoinsDisplay()
    {
        if (_totalCoinsText != null)
            _totalCoinsText.text = $"Total: {_totalCoinsEverCollected}";
    }

    /// Saves total coins to persistent storage
    public void SaveTotalCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", _totalCoinsEverCollected);
        PlayerPrefs.Save(); // Explicit save for safety
    }

    // Loads total coins from persistent storage
   
    public void LoadTotalCoins()
    {
        _totalCoinsEverCollected = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateTotalCoinsDisplay();
    }
}