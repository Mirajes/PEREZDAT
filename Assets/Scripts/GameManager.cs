using UnityEngine;
using System.Collections.Generic;
using TMPro;
/*
pishu bez podskazok tak kak kompov ne hvatilo i rabotau bez rashirenui
*/

public class GameManager : MonoBehaviour {

    [SerializeField] private GameUI _gameUI;

    [SerializeField] private float _timer = 60f;
    [SerializeField] private float _currentTimer;
    [SerializeField] private float _spawnDelay = 3f;
    [SerializeField] private float _spawnDelayThreshold = 0f;
    // [SerializeField] private float _spawnrate = 1f;

    [SerializeField] private AnonymLogic _anonymPrefab;
    [SerializeField] private Transform _anonymContainer;

    [SerializeField] private TMP_Text _timerText;

    private void Start() {
        Restart();
    }

    private void Update() {
        if (GameContext.Instance.IsPaused || GameContext.Instance.IsFinished) return;

        if (_currentTimer > 0) {
            _currentTimer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(_currentTimer / 60);
            int seconds = Mathf.FloorToInt(_currentTimer % 60);
            _timerText.text = $"{minutes:00}:{seconds:00}";
     } else {
          _timerText.text = "00:00";
           // Можно добавить обработку окончания таймера
           }

        if (_currentTimer <= 0) {
            GameContext.FinishGame?.Invoke();
            _gameUI.ShowFinishScreen();
        }

        if (_spawnDelayThreshold >= _spawnDelay) {
            _spawnDelayThreshold = 0f;
            SpawnAnonym();
        } else {
            _spawnDelayThreshold += Time.deltaTime;
        }
    }

    public void Restart() {
        _currentTimer = _timer;
        _gameUI.OnRestart();
        GameContext.Instance.Init();
        foreach (Transform item in _anonymContainer) {
            Destroy(item.gameObject);
        }
    }

    public void TogglePause() {
        GameContext.PauseGame?.Invoke();
    }

    private void SpawnAnonym() {
        AnonymLogic newAnonym = Instantiate(_anonymPrefab, _anonymContainer);
        newAnonym.transform.position = SetRandomPos();
    }

    private Vector3 SetRandomPos() {
        float randomX = UnityEngine.Random.Range(0, Screen.width);
        // float randomY = Random.Range(0, Screen.height);
        Vector3 screenPos = new Vector3(randomX, Screen.height + 5f, Camera.main.nearClipPlane + 5f); 

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPos);
        return worldPoint;
    }
}