using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    [SerializeField] private int _currentCoins = 0;
    
    public int CurrentCoins {
        get => _currentCoins;
        private set {
            _currentCoins = value;
            OnCoinsUpdated?.Invoke(_currentCoins);
        }
    }

    public delegate void CoinsUpdated(int newAmount);
    public static event CoinsUpdated OnCoinsUpdated;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount) => CurrentCoins += amount;

    public bool SpendCoins(int amount)
    {
        if (CurrentCoins >= amount) {
            CurrentCoins -= amount;
            return true;
        }
        return false;
    }
}