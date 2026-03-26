using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Dictionary<string, AudioClip> SoundsDict => _soundsDict;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Dictionary<string, AudioClip> _soundsDict = new();

    public static Action<string> PlayClip;
    public static Action<string> PlayMusic;

    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    private void Awake() {
        if (_instance == null){
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else {
            Destroy(this.gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
        Init();
    }

    private void OnEnable() {
        PlayClip += OnPlayClip;
    }
private void OnDisable() {
    PlayClip -= OnPlayClip;
}
    private void OnDestroy() {
        _instance = null;
    }

    private void Init() {
        _soundsDict.Add("exp", Resources.Load<AudioClip>("Sound/explosion"));
    }

    public void ChangeVolume(float volume) {
        _audioSource.volume = volume;
    }

    private void OnPlayClip(string clipName) {
        AudioClip clip = _soundsDict[clipName];
        _audioSource.PlayOneShot(clip);
    }
}


