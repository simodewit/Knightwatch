using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DayNightCycle : MonoBehaviour
{
    [Header("global light settings")]
    public float minusUnits;
    public float totalRotation;

    [Header("all lights settings")]
    public string lightsOnTime;
    public string tag;

    [Header("scripts and gameobjects")]
    public GameObject globalLight;
    public Timer timer;

    private bool lightsAreOn;
    public GameObject[] lights;
    private float totalSeconds;
    private float newSeconds;
    private float secondsInPercentage;

    public void Start()
    {
        totalSeconds = timer.minute * 60 + timer.second;
    }

    public void Update()
    {
        newSeconds = timer.minute * 60 + timer.second;

        secondsInPercentage = newSeconds / totalSeconds * totalRotation;
        globalLight.transform.localEulerAngles = new Vector3(secondsInPercentage - minusUnits,0,0);

        if(timer.text == lightsOnTime && lightsAreOn == false)
        {
            lightsAreOn = true;

            foreach (GameObject light in lights)
            {
                light.active = true;
            }
        }
    }
}
