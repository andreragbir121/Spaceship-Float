using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
        [SerializeField] private int _currentCoins = 0;
    
    // Public accessor with getter/setter
    public int CurrentCoins {
        get => _currentCoins;
        private set {
            _currentCoins = value;
            OnCoinsUpdated?.Invoke(_currentCoins); // Update UI/other systems
        }
    }

    // Event for UI/other systems to listen to
    public delegate void CoinsUpdated(int newAmount);
    public static event CoinsUpdated OnCoinsUpdated;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        } else {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        CurrentCoins += amount;
    }

    public bool SpendCoins(int amount)
    {
        if (CurrentCoins >= amount) {
            CurrentCoins -= amount;
            return true;
        }
        return false;
    }
}