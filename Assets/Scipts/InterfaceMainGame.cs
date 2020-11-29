using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceMainGame : MonoBehaviour
{
    public Text textModeGame;
    public Text textPoints;
    public GameObject pauseMenu;
    public GameObject pauseBtn;
    
    private bool gameIsPaused = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().volume = GameManager.sounds;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.gameMode)
        {
            case "Generated Viruses":
                textModeGame.text = "Generated Viruses: " + GameManager.currentNumberStonesThrown;
                break;
            case "Countdown":
                textModeGame.text = "Seconds: " + GameManager.currentSeconds;
                break;
            case "Survival":
                textModeGame.text = "Lives: " + GameManager.currentLives;
                break;
        }

        textPoints.text = "Score: " + GameManager.score;
    }
    
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            gameIsPaused = false;
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!gameIsPaused)
        {
            Time.timeScale = 0;
            AudioListener.volume = .5f;
            gameIsPaused = true;
            pauseBtn.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }

    public void ExitPause()
    {
        Time.timeScale = 3;
        AudioListener.volume = 1;
        gameIsPaused = false;
        pauseBtn.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 3;
        AudioListener.volume = 1;
        SceneManager.LoadScene("Main Game");
    }
    
    public void EndGame()
    {
        Time.timeScale = 3;
        AudioListener.volume = 1;
        SceneManager.LoadScene("Final");
    }

    public bool IsPauseGame()
    {
        return gameIsPaused;
    }
    
}
