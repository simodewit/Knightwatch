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
    public GameObject options;
    public GameObject credits;
    public GameObject quitGame;
    public string sceneName;

    [Header("settings")]
    public AudioMainMenu audioScript;
    public Slider mainVolume;
    public Slider SFXVolume;
    public Slider musicVolume;
    public AudioSource buttonClick;

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
            SFXVolume.SetValueWithoutNotify(PlayerPrefs.GetFloat("SFXVolume"));
        }
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(sceneName);
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

    public void OnClickYesQuitGame()
    {
        buttonClick.Play();
        Application.Quit();
    }

    public void OptionsVolume()
    {
        PlayerPrefs.SetFloat("mainVolume", mainVolume.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume.value);
        audioScript.UpdateAudioLevel();
    }

    public void OptionsResolution()
    {
        Screen.SetResolution(info[resolutions.value].width, info[resolutions.value].height, fullscreen);
    }

    public Information[] info;
}

[SerializeField]
public class Information
{
    public int width;
    public int height;
}