using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMainMenu : MonoBehaviour
{
    public AudioSource music1;
    public AudioSource music2;
    public AudioSource buttonClick;

    public void Update()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("mainVolume");
        music1.volume = PlayerPrefs.GetFloat("musicVolume");
        music2.volume = PlayerPrefs.GetFloat("musicVolume");
        buttonClick.volume = PlayerPrefs.GetFloat("SFXVolume");
    }
}
