using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class DevTool : MonoBehaviour
{
    [Header("scripts")]
    public CounterForMaterials counterForMaterials;
    public Timer timer;

    [Header("ui")]
    public GameObject panel;

    [Header("do not touch")]
    public InputAction move;
    public InputMaster input;

    #region input

    private void Awake()
    {
        input = new InputMaster();
    }

    private void OnEnable()
    {
        move = input.Movement3rdperson.DevTool;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    #endregion

    public void Update()
    {
        if(move.IsPressed() == true)
        {
            if(panel == enabled)
            {
                panel.SetActive(false);
            }
            else
            {
                panel.SetActive(true);
            }
        }
    }

    public void ButtonApply()
    {
        #region takes variables and resets

        int minutes = int.Parse(panel.transform.GetChild(1).GetComponent<InputField>().text);
        int seconds = int.Parse(panel.transform.GetChild(2).GetComponent<InputField>().text);

        int Wood = int.Parse(panel.transform.GetChild(3).GetComponent<InputField>().text);
        int stone = int.Parse(panel.transform.GetChild(4).GetComponent<InputField>().text);
        int metal = int.Parse(panel.transform.GetChild(5).GetComponent<InputField>().text);
        int coins = int.Parse(panel.transform.GetChild(6).GetComponent<InputField>().text);

        panel.transform.GetChild(3).GetComponent<InputField>().text = 0.ToString();
        panel.transform.GetChild(4).GetComponent<InputField>().text = 0.ToString();
        panel.transform.GetChild(5).GetComponent<InputField>().text = 0.ToString();
        panel.transform.GetChild(6).GetComponent<InputField>().text = 0.ToString();

        #endregion

        #region apply's all variables

        counterForMaterials.woodAmount = Wood;
        counterForMaterials.woodAmount = stone;
        counterForMaterials.woodAmount = metal;
        counterForMaterials.woodAmount = coins;

        timer.minute = minutes;
        timer.second = seconds;

        #endregion
    }
}
