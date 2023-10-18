using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsLevel1 : MonoBehaviour
{
    public AudioSource[] music;
    public AudioSource[] sfxSounds;

    public void UpdateAudioLevel()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("mainVolume");

        foreach (AudioSource source in sfxSounds)
        {
            source.volume = PlayerPrefs.GetFloat("SFXVolume");
        }

        foreach (AudioSource source in music)
        {
            source.volume = PlayerPrefs.GetFloat("musicVolume");
        }
    }
}
