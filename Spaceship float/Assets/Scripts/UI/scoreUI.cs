using UnityEngine;
using UnityEngine.UI;
public class scoreUI : MonoBehaviour
{
    [SerializeField] private Text _currentCoinsText;
    [SerializeField] private Text _totalCoinsText;

    [Header("Data")]
    [SerializeField] private int _totalCoinsEverCollected = 0;
    private int _currentSessionCoins = 0;

    void Start()
    {
        // Initialize UI
        UpdateCurrentCoinsDisplay(0);
        UpdateTotalCoinsDisplay();

        // Subscribe to events
        CurrencyManager.OnCoinsUpdated += HandleCoinChange;
    }

    void OnDestroy()
    {
        // Always unsubscribe to prevent memory leaks
        CurrencyManager.OnCoinsUpdated -= HandleCoinChange;
    }

    private void HandleCoinChange(int newAmount)
    {
        int delta = newAmount - _currentSessionCoins;
        _currentSessionCoins = newAmount;
        _totalCoinsEverCollected += delta;

        UpdateCurrentCoinsDisplay(newAmount);
        UpdateTotalCoinsDisplay();
    }

    // Current run coins (resets between sessions)
    private void UpdateCurrentCoinsDisplay(int amount)
    {
        if (_currentCoinsText != null)
            _currentCoinsText.text = $"Coins: {amount}";
    }

    // Lifetime coins (persists between sessions)
    private void UpdateTotalCoinsDisplay()
    {
        if (_totalCoinsText != null)
            _totalCoinsText.text = $"Total: {_totalCoinsEverCollected}";
    }

    // Call this when saving game data
    public void SaveTotalCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", _totalCoinsEverCollected);
    }

    // Call this when loading game data
    public void LoadTotalCoins()
    {
        _totalCoinsEverCollected = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateTotalCoinsDisplay();
    }
}