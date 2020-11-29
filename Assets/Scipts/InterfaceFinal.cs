using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceFinal : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public Text textThrown;
    public Text textScore;
    public Text textMinutesPlayed;
    public Text textSecondsPlayed;
    
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic.volume = GameManager.music;
        GetComponent<AudioSource>().volume = GameManager.sounds;
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "Score: " + GameManager.score;
        textMinutesPlayed.text = "Minutes Played: " + (int) GameManager.durationGame / 60;
        textSecondsPlayed.text = "Seconds Played: " + (int) GameManager.durationGame % 60;
        textThrown.text = "Generated Viruses: " + GameManager.currentNumberStonesThrown;
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
