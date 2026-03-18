using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    Slider _masterVolume, _musicVolume, _sfxVolume;
    UIDocument _uidoc;
    VisualElement _root;
    [SerializeField]
    SettingsDataObject _settingsData;
    [SerializeField]
    AudioMixer _audioMixer;

    void Awake()
    {
        _uidoc = GetComponent<UIDocument>();
        _root = _uidoc.rootVisualElement;

        _masterVolume = _root.Q<Slider>("sliderMaster");
        _musicVolume = _root.Q<Slider>("sliderMusic");
        _sfxVolume = _root.Q<Slider>("sliderSFX");
    }

    void Start()
    {
        LoadSettings();
        // Register callbacks
        _masterVolume.RegisterValueChangedCallback(evt => SaveSettings());
        _musicVolume.RegisterValueChangedCallback(evt => SaveSettings());
        _sfxVolume.RegisterValueChangedCallback(evt => SaveSettings());
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