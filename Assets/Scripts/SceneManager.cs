using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour
{
    public AudioManager audioManager;
    public SettingsController settingsController;

    private void Start()
    {
        audioManager = AudioManager.Instance;
        settingsController = SettingsController.Instance;
        settingsController.gameObject.SetActive(false);
    }
    public void OnStartClick()
    {
        audioManager.PlaySFX(audioManager.sfxButtonPress);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void OnSettingsClick()
    {
        audioManager.PlaySFX(audioManager.sfxButtonPress);
        settingsController.gameObject.SetActive(true);
    }

    public void OnBackClick()
    {
        audioManager.PlaySFX(audioManager.sfxButtonPress);
        settingsController.gameObject.SetActive(false);
    }

    public void OnQuitClick()
    {
        audioManager.PlaySFX(audioManager.sfxButtonPress);
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void OnMenuClick()
    {
        audioManager.PlaySFX(audioManager.sfxButtonPress);
        GameManager.Instance.PauseGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void OnResumeClick()
    {
        audioManager.PlaySFX(audioManager.sfxButtonPress);
        GameManager.Instance.PauseGame();
    }
}