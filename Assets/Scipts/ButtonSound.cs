using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip btnClick;
    
    public AudioClip btnClick2;

    private AudioSource _audioSource;

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Click1()
    {
        _audioSource.clip = btnClick;
        _audioSource.Play();
    }

    public void Click2()
    {
        _audioSource.clip = btnClick2;
        _audioSource.Play();
    }
    
}
