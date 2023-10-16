using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DayNightCycle : MonoBehaviour
{
    public Timer timer;
    public GameObject light;
    public float minusUnits;
    public float totalRotation;

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
        light.transform.localEulerAngles = new Vector3(secondsInPercentage - minusUnits,0,0);
    }
}
