using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float[] timePerLevel;
    public int level;
    public bool buttonIsPressed;
    public float timer;
    public GameObject button;

    public void DoUpdate()
    {
        if(buttonIsPressed == true)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Init()
    {
        timer = timePerLevel[level];
    }

    public void ButtonPress()
    {
        buttonIsPressed = true;
        button.SetActive(false);
    }
}
