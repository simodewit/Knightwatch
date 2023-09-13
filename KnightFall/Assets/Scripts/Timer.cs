using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float[] timePerLevel;
    public int level;
    public bool buttonIsPressed;
    public float timer;

    public void DoUpdate()
    {
        if(buttonIsPressed == true)
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0)
        {
            timer = 0;
            buttonIsPressed = false;
        }
    }

    public void Init()
    {
        timer = timePerLevel[level];
    }
}
