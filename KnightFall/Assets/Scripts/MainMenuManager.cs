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
    public GameObject main;
    public GameObject options;
    public GameObject credits;
    public GameObject quitGame;
    public string sceneName;

    public AudioSource buttonClick;
    public Slider mainVolume;
    public Slider SFXVolume;
    public Slider musicVolume;
    public Dropdown resolutions;
    public Toggle fullscreen;

    public void OnClickPlay()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnClickOptions()
    {
        main.SetActive(false);
        options.SetActive(true);
        
    }

    public void OnClickCredits()
    {
        main.SetActive(false);
        credits.SetActive(true);
    }

    public void OnClickQuitGame()
    {
        main.SetActive(false);
        quitGame.SetActive(true);
    }

    public void OnClickOptionsBack()
    {
        options.SetActive(false);
        main.SetActive(true);
    }

    public void OnClickCreditsBack()
    {
        credits.SetActive(false);
        main.SetActive(true);
    }

    public void OnClickNoQuitGame()
    {
        quitGame.SetActive(false);
        main.SetActive(true);
    }

    public void OnClickYesQuitGame()
    {
        Application.Quit();
    }

    public void OptionsVolume()
    {
        AudioListener.volume = mainVolume.value;

        PlayerPrefs.SetFloat("Volume", mainVolume.value);

        mainVolume.value = PlayerPrefs.GetFloat("Volume");

        //music volume aanpassen
        //FSX volume aanpassen
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