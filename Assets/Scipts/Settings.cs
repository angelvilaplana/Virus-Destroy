using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource sounds;
    
    public Dropdown gameMode;
    public Text gameModeText;
    public InputField inputOption;
    public Slider optionMusic;
    public Slider optionSound;
    
    void Start()
    {
        gameMode.value = GetGameMode();
        ChangeTextGameMode();
        changeGameModeInput();
        
        optionMusic.value = GameManager.music;
        optionSound.value = GameManager.sounds;
        
        backgroundMusic.volume = GameManager.music;
        sounds.volume = GameManager.sounds;
    }

    private int GetGameMode()
    {
        List<Dropdown.OptionData> optionsModeGame = gameMode.options;
        for (var i = 0; i < optionsModeGame.Count; i++)
        {
            var optionModeGame = optionsModeGame[i];
            if (optionModeGame.text.Equals(GameManager.gameMode))
            {
                return i;
            }
        }
        return -1;
    }

    private void changeGameModeInput()
    {
        switch (GameManager.gameMode)
        {
            case "Generated Viruses":
                inputOption.text = GameManager.numberStones.ToString();
                break;
            case "Countdown":
                inputOption.text = GameManager.seconds.ToString();
                break;
            case "Survival":
                inputOption.text = GameManager.lives.ToString();
                break;
        }
    }

    private void ChangeTextGameMode()
    {
        switch (GameManager.gameMode)
        {
            case "Generated Viruses":
                gameModeText.text = "Viruses: ";
                break;
            case "Countdown":
                gameModeText.text = "Seconds: ";
                break;
            case "Survival":
                gameModeText.text = "Lives: ";
                break;
        }
    }

    public void changeGameMode()
    {
        var newOption = gameMode.options[gameMode.value].text;
        GameManager.gameMode = newOption;
        PlayerPrefs.SetString("Game Mode", newOption);
        ChangeTextGameMode();
        changeGameModeInput();
    }

    public void ChangeMusic()
    {
        GameManager.music = optionMusic.value;
        PlayerPrefs.SetFloat("Music Volume", GameManager.music);
        backgroundMusic.volume = GameManager.music;
    }
    
    public void ChangeOptionInput()
    {
        var value = Convert.ToInt32(inputOption.text);
        if (value < 1)
        {
            changeGameModeInput();
            return;
        }
        
        if (GameManager.gameMode == "Generated Viruses")
        {
            GameManager.numberStones = value;
            PlayerPrefs.SetInt("Number Stones", value);
        } else if (GameManager.gameMode == "Countdown")
        {
            GameManager.seconds = value;
            PlayerPrefs.SetInt("Seconds", value);
        } else if (GameManager.gameMode == "Survival")
        {
            GameManager.lives = value;
            PlayerPrefs.SetInt("Lives", value);
        }
    }
    
    public void ChangeSound()
    {
        GameManager.sounds = optionSound.value;
        PlayerPrefs.SetFloat("Sound Volume", GameManager.sounds);
        sounds.volume = GameManager.sounds;
    }
}
