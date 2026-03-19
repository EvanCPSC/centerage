using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] PlayerMovement playerController;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] public GameObject[] items, enemies;
    public bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
        pauseCanvas.gameObject.SetActive(false);
        //gameOverCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        //gameOverCanvas.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        //gameOverCanvas.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if (isPaused) // calling when is paused, going to unpause it
        {
            Time.timeScale = 1;
            pauseCanvas.gameObject.SetActive(false);
            isPaused = false;
        } else // calling when is unpaused, going to pause it
        {
            Time.timeScale = 0;
            pauseCanvas.gameObject.SetActive(true);
            isPaused = true;
        }
    }
}
