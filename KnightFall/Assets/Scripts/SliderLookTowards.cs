using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderLookTowards : MonoBehaviour
{
    public Camera camera;

    public void Start()
    {
        camera = Camera.main;
    }

    public void Update()
    {
        transform.LookAt(camera.transform.position);
    }
}
