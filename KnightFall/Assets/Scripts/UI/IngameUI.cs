using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;

public class IngameUI : MonoBehaviour
{
    [Header("panels")]
    public GameObject mainPanel;
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

    [Header("animation")]
    public Animator animator;
    public GameObject slidingPanel;

    [Header("other things")]
    public string sceneToLoadIfExit;
    public SoundsLevel1 audioScript;
    public AudioSource buttonClick;
    public PlayerAttack playerAttackScript;
    public ResolutionsInfo[] information;

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

    #region buttonActions

    public void InputInteraction(CallbackContext c)
    {
        if(c.started)
        {
            if (options.active)
            {
                buttonClick.Play();
                options.SetActive(false);
                mainPanel.SetActive(true);
                Time.timeScale = 1;
                playerAttackScript.enabled = true;
            }
            else if (settings.active)
            {
                buttonClick.Play();
                options.SetActive(true);
                settings.SetActive(false);
            }
            else if (controls.active)
            {
                buttonClick.Play();
                options.SetActive(true);
                controls.SetActive(false);
            }
            else if (backToMenu.active)
            {
                buttonClick.Play();
                options.SetActive(true);
                backToMenu.SetActive(false);
            }
            else
            {
                buttonClick.Play();
                mainPanel.SetActive(false);
                options.SetActive(true);
                Time.timeScale = 0;
                playerAttackScript.enabled = false;
            }
        }
    }

    public void SettingsButton()
    {
        buttonClick.Play();
        options.SetActive(false);
        settings.SetActive(true);
    }
    public void ControlsButton()
    {
        buttonClick.Play();
        options.SetActive(false);
        controls.SetActive(true);
    }
    public void QuitGameButton()
    {
        buttonClick.Play();
        options.SetActive(false);
        backToMenu.SetActive(true);
    }

    public void ResumeButton()
    {
        buttonClick.Play();
        options.SetActive(false);
        mainPanel.SetActive(true);
        Time.timeScale = 1;
        playerAttackScript.enabled = true;
    }
    public void BackButtonSettings()
    {
        buttonClick.Play();
        settings.SetActive(false);
        options.SetActive(true);
    }
    public void BackButtonControls()
    {
        buttonClick.Play();
        controls.SetActive(false);
        options.SetActive(true);
    }
    public void BackButtonQuitGame()
    {
        buttonClick.Play();
        backToMenu.SetActive(false);
        options.SetActive(true);
    }
    public void BackToMainMenuButton()
    {
        buttonClick.Play();
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
        //if (PlayerPrefs.GetInt("resolutions") != 0)
        //{
        //    resolutions.value = PlayerPrefs.GetInt("resolutions");
        //}
        //Screen.SetResolution(information[resolutions.value].width, information[resolutions.value].height, fullscreen);
    }

    #region settings

    public void OptionsVolume()
    {
        PlayerPrefs.SetFloat("mainVolume", mainVolume.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume.value);
        audioScript.UpdateAudioLevel();
    }

    public void OptionsResolution()
    {
        Screen.SetResolution(information[resolutions.value].width, information[resolutions.value].height, fullscreen);
    }

    #endregion

    public void PanelIn()
    {
        if (slidingPanel != null && animator != null)
        {
            bool IsOpen = animator.GetBool("OutAndIn");

            animator.SetBool("OutAndIn", !IsOpen);
        }
    }
}

[System.Serializable]

public class ResolutionsInfo
{
    public int width;
    public int height;
}
