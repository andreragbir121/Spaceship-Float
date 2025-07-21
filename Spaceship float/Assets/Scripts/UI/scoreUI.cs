using UnityEngine;
using UnityEngine.UI;

public class scoreUI : MonoBehaviour
{
    [SerializeField] private Text _currentCoinsText;
    [SerializeField] private Text _totalCoinsText;
    
    private int _totalCoinsEverCollected = 0;
    private int _currentSessionCoins = 0;

    void Start()
    {
        // Load saved total when starting
        LoadTotalCoins();
        UpdateCurrentCoinsDisplay(0);
        UpdateTotalCoinsDisplay();
        
        CurrencyManager.OnCoinsUpdated += HandleCoinChange;
    }

    void OnDestroy()
    {
        CurrencyManager.OnCoinsUpdated -= HandleCoinChange;
        SaveTotalCoins(); // Save when object is destroyed
    }

    private void HandleCoinChange(int newAmount)
    {
        int delta = newAmount - _currentSessionCoins;
        _currentSessionCoins = newAmount;
        _totalCoinsEverCollected += delta;

        UpdateCurrentCoinsDisplay(newAmount);
        UpdateTotalCoinsDisplay();
        SaveTotalCoins(); // Save after each change
    }

    private void UpdateCurrentCoinsDisplay(int amount)
    {
        if (_currentCoinsText != null)
            _currentCoinsText.text = $"Coins: {amount}";
    }

    private void UpdateTotalCoinsDisplay()
    {
        if (_totalCoinsText != null)
            _totalCoinsText.text = $"Total: {_totalCoinsEverCollected}";
    }

    public void SaveTotalCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", _totalCoinsEverCollected);
        PlayerPrefs.Save();
    }

    public void LoadTotalCoins()
    {
        // Only load if there's saved data, otherwise keep default 0
        if (PlayerPrefs.HasKey("TotalCoins"))
        {
            _totalCoinsEverCollected = PlayerPrefs.GetInt("TotalCoins");
        }
    }
}