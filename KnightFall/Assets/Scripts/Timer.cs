using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject button;
    public float minute;
    public float second;
    public GameObject counter;

    [Header("DoNotTouch")]
    public bool buttonIsPressed;
    public string text;

    public void Update()
    {
        if(buttonIsPressed == true)
        {
            if(second <= 0)
            {
                if(minute <= 0)
                {
                    //stop game
                }
                else
                {
                    minute -= 1;
                    second = 59;
                }
            }
            else
            {
                second -= 1 * Time.deltaTime;
            }

            int fixedMinute = Mathf.RoundToInt(minute);
            int fixedSecond = Mathf.RoundToInt(second);
            convertToTime(fixedMinute, fixedSecond, text);
            counter.GetComponent<TextMeshProUGUI>().text = text;
        }
    }

    public void Init()
    {
        buttonIsPressed = true;
    }

    public void ButtonPress()
    {
        button.SetActive(false);
    }

    public void convertToTime(int minutes, int seconds, string endResult)
    {
        endResult = minutes.ToString() + ":" + seconds.ToString();
    }
}