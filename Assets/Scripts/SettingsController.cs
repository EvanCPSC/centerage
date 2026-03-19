using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SettingsController : MonoBehaviour
{
    public static SettingsController Instance { get; private set; }
    public Slider _masterVolume, _musicVolume, _sfxVolume;
    [SerializeField]
    SettingsDataObject _settingsData;
    [SerializeField]
    AudioMixer _audioMixer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _masterVolume = transform.Find("MasterSlider").GetComponent<Slider>();
            _musicVolume = transform.Find("MusicSlider").GetComponent<Slider>();
            _sfxVolume = transform.Find("SFXSlider").GetComponent<Slider>();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        LoadSettings();
        // Register callbacks
        _masterVolume.onValueChanged.AddListener(delegate { SaveSettings(); });
        _musicVolume.onValueChanged.AddListener(delegate { SaveSettings(); });
        _sfxVolume.onValueChanged.AddListener(delegate { SaveSettings(); });
    }

    void LoadSettings()
    {
        _settingsData.LoadSettings();
        _masterVolume.value = _settingsData.MasterVolume;
        _musicVolume.value = _settingsData.MusicVolume;
        _sfxVolume.value = _settingsData.SFXVolume;
        ApplySettings();
    }

    void SaveSettings()
    {
        _settingsData.MasterVolume = _masterVolume.value;
        _settingsData.MusicVolume = _musicVolume.value;
        _settingsData.SFXVolume = _sfxVolume.value;

        // Save the settings data
        PlayerPrefs.SetString("SettingsData", JsonUtility.ToJson(_settingsData));
        PlayerPrefs.Save();
        ApplySettings();
    }

    void ApplySettings()
    {
        // Apply the settings to the audio mixer groups
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10((_settingsData.MasterVolume + .0001f) / 100) * 20);
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10((_settingsData.MusicVolume + .0001f) / 100) * 20);
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10((_settingsData.SFXVolume + .0001f) / 100) * 20);
        PlayerPrefs.Save();
    }

}