using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Slider _soundSlider;

    private void OnEnable() {
        _soundSlider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable() {
        _soundSlider.onValueChanged.RemoveAllListeners();
    }

    private void OnValueChanged(float value) {
        AudioManager.Instance.ChangeVolume(value);
    }
}
