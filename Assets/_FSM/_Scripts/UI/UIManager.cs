using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text livesText;

    private void OnEnable()
    {
        ActionEvents.OnLivesChanged += UpdateLivesUI;
    }

    private void OnDisable()
    {
        ActionEvents.OnLivesChanged -= UpdateLivesUI;
    }

    private void UpdateLivesUI(int currentLives)
    {
        livesText.text = $"Lives: {currentLives}";
    }
}
