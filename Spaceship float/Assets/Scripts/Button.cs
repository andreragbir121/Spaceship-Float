using UnityEngine;
using UnityEngine.UI;

public class LockButtonController : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite unlockedSprite;

    [Header("Currency Settings")]
    [SerializeField] private int unlockCost = 0;

    private bool isLocked = true;
    private bool isUnlocked = false;
    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        UpdateIcon();
    }

    public void OnLockButtonClick()
    {
        if (isLocked)
        {
            if (CanAffordUnlock())
            {
                Unlock();
            }
            else
            {
                Debug.Log("Not enough currency!");
            }
        }
        else if (isUnlocked)
        {
            StartGame();
        }
    }

    private bool CanAffordUnlock()
    {
        // 🧪 Always returns true for testing
        return true;
    }

    private void Unlock()
    {
        isLocked = false;
        isUnlocked = true;
        UpdateIcon();

        // Future currency deduction
        // CurrencyManager.Instance.SpendCurrency(unlockCost);
    }

    private void UpdateIcon()
    {
        buttonImage.sprite = isLocked ? lockedSprite : unlockedSprite;
    }

    private void StartGame()
    {
        isUnlocked = false;
        Debug.Log("Game Started!");
        // TODO: Replace with your actual start logic (load scene, enable gameplay, etc.)
    }
}
