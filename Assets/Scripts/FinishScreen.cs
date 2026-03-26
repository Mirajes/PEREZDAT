using UnityEngine;
using TMPro;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    public void UpdateScreen() {
        var gameContext = GameContext.Instance;
        _scoreText.text = $"СЧЁТ: {gameContext.CurrentScore}";
    }
}
