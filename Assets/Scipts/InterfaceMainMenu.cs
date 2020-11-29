using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class InterfaceMainMenu : MonoBehaviour
{
    public VideoPlayer backgroundVideo;
    public Animator main;
    public Animator settings;
    public Animator credits;
    public Animator howToPlay;

    private bool isStartAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.score = 0;
        GameManager.currentNumberStonesThrown = 0;
    }

    void Update()
    {
        if (!isStartAnimation && backgroundVideo.isPlaying)
        {
            main.SetTrigger("videoStart");
            isStartAnimation = true;
        }
        
        if (credits.GetCurrentAnimatorStateInfo(0).IsName("CreditsAnimationEnd"))
        {
            main.SetBool("backCredits", true);
        } else if (main.GetCurrentAnimatorStateInfo(0).IsName("MainAnimation_Start"))
        {
            main.SetBool("backCredits", false);
        }
    }

    public void ClickStartGame()
    {
        SceneManager.LoadScene("Main Game");
    }
    
    public void ClickSettings()
    {
        main.SetTrigger("goSettings");
        settings.SetTrigger("show");
    }
    
    public void ClickCredits()
    {
        main.SetTrigger("goCredits");
        credits.SetTrigger("show");
    }
    
    public void ClickHowToPlay()
    {
        main.SetTrigger("goHowToPlay");
        howToPlay.SetTrigger("show");
    }
    
    public void ClickBackMenu(string option)
    {
        switch (option)
        {
            case "Settings":
                settings.SetTrigger("back");
                main.SetTrigger("backSettings");
                break;
            case "Credits":
                credits.SetTrigger("back");
                break;
            case "How To Play":
                howToPlay.SetTrigger("back");
                main.SetTrigger("backHowToPlay");
                break;
        }
    }

}
