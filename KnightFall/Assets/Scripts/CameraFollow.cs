using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public Vector3 offset;
    public float lerpspeed;
    public GameObject empty;
    public InputMaster input;
    public InputAction move;

    private void Awake()
    {
        input = new InputMaster();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        move = input.Movement3rdperson.Movement;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {   
        Vector3 playerPositionWithOffset = new Vector3(player.transform.position.x +offset.x, player.transform.position.y +offset.y, player.transform.position.z +offset.z);
        transform.position = Vector3.Lerp(transform.position, playerPositionWithOffset, lerpspeed * Time.deltaTime);
    }
}
