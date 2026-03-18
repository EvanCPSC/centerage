using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour
{
    public AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;
    }
    public void OnStartClick()
    {
        audioManager.PlaySFX(audioManager.sfxButtonPress);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void OnSettingsClick()
    {
        audioManager.PlaySFX(audioManager.sfxButtonPress);
        
    }

    public void OnQuitClick()
    {
        audioManager.PlaySFX(audioManager.sfxButtonPress);
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}