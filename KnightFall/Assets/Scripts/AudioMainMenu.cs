using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioMainMenu : MonoBehaviour
{
    public AudioSource[] music;
    public AudioSource[] SFXSounds;
    public bool randomizeTheMusic;
    private int randomized;

    public void Start()
    {
        print(music.Length);
        print(randomized);
        //UpdateAudioLevel();
    }

    public void Update()
    {
        ChooseSong();
    }

    public void UpdateAudioLevel()
    {
        print("updates audio");
        AudioListener.volume = PlayerPrefs.GetFloat("mainVolume");

        foreach (AudioSource source in SFXSounds)
        {
            source.volume = PlayerPrefs.GetFloat("SFXVolume");
        }

        foreach (AudioSource source in music)
        {
            source.volume = PlayerPrefs.GetFloat("musicVolume");
        }
    }

    public void ChooseSong()
    {
        if (music[randomized].isPlaying == true || (music[randomized].isPlaying == false && !Application.isFocused))
            return;                     

        print("new number");

        if (randomizeTheMusic)
        {
            RandomizeMusic();
        }
        else
        {
            normalMode();
        }
    }

    public void RandomizeMusic()
    {
        randomized = Random.Range(0, music.Length);
        music[randomized].Play();
    }

    public void normalMode()
    {
        if(randomized >= music.Length - 1)
        {
            randomized = 0;
            music[randomized].Play();
        }
        else
        {
            randomized += 1;
            music[randomized].Play();
        }
    }
}
