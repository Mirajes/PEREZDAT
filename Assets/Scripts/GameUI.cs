using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private RectTransform _overlay;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private FinishScreen _finishScreen;

    public void OnRestart() {
        _overlay.gameObject.SetActive(true);
        _finishScreen.gameObject.SetActive(false);
    }

    public void ShowFinishScreen() {
        _overlay.gameObject.SetActive(false);
        _finishScreen.gameObject.SetActive(true);
        _finishScreen.UpdateScreen();
    }

    private void Update() {
        var gameContext = GameContext.Instance;
        _scoreText.text = $"СЧЁТ: {gameContext.CurrentScore}";
    }
}
