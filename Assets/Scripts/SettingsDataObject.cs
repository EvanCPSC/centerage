using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObjects/SettingsData")]
public class SettingsDataObject : ScriptableObject
{
    public float MasterVolume = 1.0f;
    public float MusicVolume = 1.0f;
    public float SFXVolume = 1.0f;

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("SettingsData") == true)
        {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("SettingsData", "{}"), this);
        }
    }
}
