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

    public void Init()
    {
        if(buttonIsPressed == true)
        {
            if(second < 0)
            {
                if(minute > 0)
                {
                    minute -= 1;
                    second = 60;
                }
                else
                {
                    print("stop game");
                }
            }
            else
            {
                second -= 1 * Time.deltaTime;
            }

            int fixedMinute = Mathf.RoundToInt(minute);
            int fixedSecond = Mathf.RoundToInt(second);
            convertToTime(fixedMinute, fixedSecond);
            counter.GetComponent<TextMeshProUGUI>().text = text;
        }
    }

    public void ButtonPress()
    {
        counter.SetActive(true);
        buttonIsPressed = true;
        button.SetActive(false);
    }

    public void convertToTime(int minutes, int seconds)
    {
        if(seconds < 10)
        {
            text = minutes.ToString() + ":" + 0 + seconds.ToString();
        }
        else
        {
            text = minutes.ToString() + ":" + seconds.ToString();
        }
        if(seconds == 60)
        {
            int newMinutes = minutes + 1;
            text = newMinutes.ToString() + ":" + "00";
        }
    }
}