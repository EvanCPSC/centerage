using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] SettingsDataObject _settingsData;
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] AudioSource _musicSource;
    public AudioClip musicMainMenu, musicCaverns;
    [SerializeField] AudioSource _sfxSource;
    public AudioClip sfxButtonPress, sfxGetItem, sfxPlayerHit, sfxEnemyDie;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _musicSource = transform.GetChild(0).GetComponent<AudioSource>();
            _sfxSource = transform.GetChild(1).GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Load settings
        _settingsData.LoadSettings();
        // Apply settings to audio sources
        _musicSource.volume = _settingsData.MusicVolume;
        _sfxSource.volume = _settingsData.SFXVolume;
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10((_settingsData.MasterVolume + .0001f) / 100) * 20);
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10((_settingsData.MusicVolume + .0001f) / 100) * 20);
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10((_settingsData.SFXVolume + .0001f) / 100) * 20);
        
    }

    void OnEnable() 
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() 
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // THIS replaces your Update loop logic
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0:
                PlayMusic(musicMainMenu);
                break;
            case 1:
                PlayMusic(musicCaverns);
                PlayerStats.SetDefaultStats();
                break;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        else if (_musicSource.clip == clip && _musicSource.isPlaying)
        {
            return;
        }
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            _sfxSource.PlayOneShot(clip);
        }
    }
    
}
