using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float[] timePerLevel;
    public int level;
    public bool buttonIsPressed;
    public float timer;

    private bool giveTimeFromArray;

    public void Update()
    {
        if(buttonIsPressed == true)
        {
            if(giveTimeFromArray == false)
            {
                giveTimeFromArray = true;
                timer = timePerLevel[level];
                //hier de knop ui uit zetten
            }
            timer -= Time.deltaTime;
        }

        if(timer <= 0)
        {
            timer = 0;
            buttonIsPressed = false;
        }
    }
}
