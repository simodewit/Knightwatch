using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Buttons/loading scene")]
    public GameObject main;
    public GameObject levelPick;
    public GameObject options;
    public GameObject credits;
    public GameObject quitGame;
    public string sceneName;

    [Header("settings")]
    public AudioMainMenu audioScript;
    public Slider mainVolume;
    public Slider sfxVolume;
    public Slider musicVolume;
    public AudioSource buttonClick;
    public Resolutions[] info;

    private Dropdown resolutions;
    private Toggle fullscreen;

    public void Start()
    {
        GetValues();
        OptionsVolume();
    }

    public void GetValues()
    {
        if (PlayerPrefs.GetFloat("mainVolume") != 0.5f)
        {
            mainVolume.SetValueWithoutNotify(PlayerPrefs.GetFloat("mainVolume"));
        }
        if (PlayerPrefs.GetFloat("musicVolume") != 0.5f)
        {
            musicVolume.SetValueWithoutNotify(PlayerPrefs.GetFloat("musicVolume"));
        }
        if (PlayerPrefs.GetFloat("SFXVolume") != 0.5f)
        {
            sfxVolume.SetValueWithoutNotify(PlayerPrefs.GetFloat("SFXVolume"));
        }
        //if (PlayerPrefs.GetInt("fullscreen") == 0)
        //{
        //    fullscreen.isOn = false;
        //}
        //else
        //{
        //    fullscreen.isOn = true;
        //}
        //if(PlayerPrefs.GetInt("resolutions") != 0)
        //{
        //    resolutions.value = PlayerPrefs.GetInt("resolutions");
        //}
        //Screen.SetResolution(info[resolutions.value].width, info[resolutions.value].height, fullscreen);
    }

    public void OnClickPlay()
    {
        main.SetActive(false);
        levelPick.SetActive(true);
        buttonClick.Play();
    }

    public void OnClickOptions()
    {
        main.SetActive(false);
        options.SetActive(true);
        buttonClick.Play();
    }

    public void OnClickCredits()
    {
        main.SetActive(false);
        credits.SetActive(true);
        buttonClick.Play();
    }

    public void OnClickQuitGame()
    {
        main.SetActive(false);
        quitGame.SetActive(true);
        buttonClick.Play();
    }

    public void OnClickOptionsBack()
    {
        options.SetActive(false);
        main.SetActive(true);
        buttonClick.Play();
    }

    public void OnClickCreditsBack()
    {
        credits.SetActive(false);
        main.SetActive(true);
        buttonClick.Play();
    }

    public void OnClickNoQuitGame()
    {
        quitGame.SetActive(false);
        main.SetActive(true);
        buttonClick.Play();
    }

    public void OnClickLevel1()
    {
        buttonClick.Play();
        SceneManager.LoadScene("Level1");
    }

    public void OnClickLevel2()
    {
        buttonClick.Play();
        SceneManager.LoadScene("Level2");
    }

    public void OnClickBackLevelPick()
    {
        levelPick.SetActive(false);
        main.SetActive(true);
    }

    public void OnClickYesQuitGame()
    {
        buttonClick.Play();
        Application.Quit();
    }

    public void OptionsVolume()
    {
        PlayerPrefs.SetFloat("mainVolume", mainVolume.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume.value);
        audioScript.UpdateAudioLevel();
    }

    public void OptionsResolution()
    {
        Screen.SetResolution(info[resolutions.value].width, info[resolutions.value].height, fullscreen);
    }
}

[System.Serializable]
public class Resolutions
{
    public int width;
    public int height;
}