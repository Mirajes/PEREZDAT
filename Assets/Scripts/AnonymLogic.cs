using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class AnonymLogic : MonoBehaviour
{
    [SerializeField] private GameManager _gm;
    [SerializeField] private int _scoreByDead = 1;
    [SerializeField] private float _aliveTime = 0f;
    [SerializeField] private float _maxTimeToDie = 15f;
    [SerializeField] private float _timeToDie;

    [SerializeField] private float _moveSpeed = 1f;

    private void OnEnable() {
        _timeToDie = Random.Range(0, _maxTimeToDie);
    }

    private void OnMouseDown() {
        if (GameContext.Instance.IsPaused || GameContext.Instance.IsFinished) return;

        GameContext.ScoreChange?.Invoke(_scoreByDead);
        DestroyObject();
    }

    private void Move() {
        transform.position = new Vector3(transform.position.x, transform.position.y - _moveSpeed * Time.deltaTime, transform.position.z);
    }

    private void Update() {
        if (GameContext.Instance.IsPaused || GameContext.Instance.IsFinished) return;

        _aliveTime += Time.deltaTime;

        if (_aliveTime >= _timeToDie){
            DestroyObject();
        }
        else {
            Move();

        }
    }

    private void DestroyObject() {
        Destroy(this.gameObject);
        AudioManager.PlayClip?.Invoke("exp");
    }
}
