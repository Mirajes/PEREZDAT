using UnityEngine;
using System;

public class GameContext : MonoBehaviour
{
    public static GameContext Instance => _instance;
    private static GameContext _instance;

    public int HighScore => _highScore;
    public int CurrentScore => _currentScore; 

    [SerializeField] private int _highScore = 0;
    [SerializeField] private int _currentScore = 0;

    public bool IsPaused => _isPaused;
    public bool IsFinished => _isFinished;

    [SerializeField] private bool _isPaused = false;
    [SerializeField] private bool _isFinished = false;

    public static Action<int> HighScoreChange;
    public static Action<int> ScoreChange;
    public static Action ScoreReset;
    public static Action FinishGame;
    public static Action PauseGame;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy() {
        _instance = null;
    }

    private void OnEnable() {
        HighScoreChange += OnHighScoreChange;
        ScoreChange += OnScoreChange;
        ScoreReset += OnScoreReset;
        PauseGame += OnPauseGame;
        FinishGame += OnFinishGame;
    }

    private void OnDisable() {
        HighScoreChange -= OnHighScoreChange;
        ScoreChange -= OnScoreChange;
        ScoreReset -= OnScoreReset;
        PauseGame -= OnPauseGame;
        FinishGame -= OnFinishGame;
    }

    public void Init() {
        _currentScore = 0;
        _isPaused = false;
        _isFinished = false;
    }

    public void OnPauseGame() {
        _isPaused = !_isPaused;
    }

    public void OnFinishGame() {
        _isFinished = true;
    }

    public void OnHighScoreChange(int currentScore) {
        if (currentScore > _highScore)
            _highScore = currentScore;
    }

    public void OnScoreChange(int amount) {
        _currentScore += amount;
    }

    public void OnScoreReset() {
        OnScoreChange(_currentScore);
    }
}