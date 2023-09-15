using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public Vector3 offset;
    public float lerpspeed;


    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {   
        Vector3 v = new Vector3(player.transform.position.x +offset.x, player.transform.position.y +offset.y, player.transform.position.z +offset.z);
        transform.position = Vector3.Lerp(transform.position, v, lerpspeed * Time.deltaTime);
    }
}
