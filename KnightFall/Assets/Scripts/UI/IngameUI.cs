using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class IngameUI : MonoBehaviour
{
    [Header("panels")]
    public GameObject options;
    public GameObject settings;
    public GameObject controls;
    public GameObject backToMenu;
    

    [Header("settings")]
    public Slider mainVolume;
    public Slider musicVolume;
    public Slider sfxVolume;
    public Toggle fullscreen;
    public Dropdown resolutions;

    [Header("other things")]
    public string sceneToLoadIfExit;
    public Information[] info;
    public SoundsLevel1 audioScript;
    public AudioSource buttonClick;

    private InputMaster input;
    private InputAction esc;

    #region input

    private void Awake()
    {
        input = new InputMaster();
    }

    private void OnEnable()
    {
        esc = input.Movement3rdperson.EscMenus;
        esc.Enable();
    }

    private void OnDisable()
    {
        esc.Disable();
    }

    #endregion

    public void Start()
    {
        GetAllValues();
    }

    public void Update()
    {
        OptionsVolume();
    }

    #region buttonActions

    public void InputInteraction(CallbackContext c)
    {
        if(c.started)
        {
            if (options.active)
            {
                options.SetActive(false);
                Time.timeScale = 1;
            }
            else if (settings.active)
            {
                options.SetActive(true);
                settings.SetActive(false);
            }
            else if (controls.active)
            {
                options.SetActive(true);
                controls.SetActive(false);
            }
            else if (backToMenu.active)
            {
                options.SetActive(true);
                backToMenu.SetActive(false);
            }
            else
            {
                options.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void SettingsButton()
    {
        options.SetActive(false);
        settings.SetActive(true);
    }
    public void ControlsButton()
    {
        controls.SetActive(false);
        settings.SetActive(true);
    }
    public void QuitGameButton()
    {
        backToMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void ResumeButton()
    {
        options.SetActive(false);
        Time.timeScale = 1;
    }
    public void BackButtonSettings()
    {
        settings.SetActive(false);
        options.SetActive(true);
    }
    public void BackButtonControls()
    {
        controls.SetActive(false);
        options.SetActive(true);
    }
    public void BackButtonQuitGame()
    {
        backToMenu.SetActive(false);
        options.SetActive(true);
    }
    public void BackToMainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneToLoadIfExit);
    }

    #endregion

    public void GetAllValues()
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
            sfxVolume.SetValueWithoutNotify(PlayerPrefs.GetFloat("sfxVolume"));
        }
        if (PlayerPrefs.GetInt("fullscreen") == 0)
        {
            fullscreen.isOn = false;
        }
        else
        {
            fullscreen.isOn = true;
        }
        if (PlayerPrefs.GetInt("resolutions") != 0)
        {
            resolutions.value = PlayerPrefs.GetInt("resolutions");
        }
        Screen.SetResolution(info[resolutions.value].width, info[resolutions.value].height, fullscreen);
    }

    #region settings

    public void OptionsVolume()
    {
        PlayerPrefs.SetFloat("mainVolume", mainVolume.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume.value);
        audioScript.UpdateAudioLevel();
    }

    public void OptionsResolution()
    {
        Screen.SetResolution(info[resolutions.value].width, info[resolutions.value].height, fullscreen);
    }

    #endregion
}
