using UnityEngine;
using UnityEngine.UI;

public class LockButtonController : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite unlockedSprite;
    [SerializeField] private Image buttonImage;

    [Header("Pricing")]
    [SerializeField] private int unlockCost = 20; // Set to 20 coins as required

    private bool isLocked = true;

    private void Awake()
    {
        // Auto-get reference if not set in Inspector
        if (buttonImage == null) buttonImage = GetComponent<Image>();
        UpdateVisuals();
    }

    public void TryUnlock()
    {
        if (!isLocked) 
        {
            StartGame();
            return;
        }

        if (CurrencyManager.Instance.CurrentCoins >= unlockCost)
        {
            Unlock();
        }
        else
        {
            Debug.Log($"Need {unlockCost} coins to unlock!");
            // Add visual/audio feedback here
        }
    }

    private void Unlock()
    {
        isLocked = false;
        CurrencyManager.Instance.SpendCoins(unlockCost);
        UpdateVisuals();
    }

    private void StartGame()
    {
        Debug.Log("Loading map...");
        // SceneManager.LoadScene("MapName"); // Uncomment when ready
    }

    private void UpdateVisuals()
    {
        buttonImage.sprite = isLocked ? lockedSprite : unlockedSprite;
        // Optional: Change button color/interactable state
    }
}