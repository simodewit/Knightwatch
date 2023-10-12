using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class CameraFollow : MonoBehaviour
{
    [Header("conditions")]
    public float scrollSpeed;
    public float lerpspeed;
    public Vector3 offset;

    [Header("do not touch")]
    public GameObject player;
    public Rigidbody rb;
    public GameObject empty;
    public InputMaster input;
    public InputAction move;

    private void Awake()
    {
        input = new InputMaster();
    }

    private void OnEnable()
    {
        move = input.Movement3rdperson.MouseScroll;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    public void Start()
    {
        empty = transform.parent.gameObject;
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        float scrollPosition = move.ReadValue<float>();
        
        if(scrollPosition != 0f)
        {
            print(scrollPosition);
            Vector3 moveAxis = new Vector3(0, 0, scrollPosition);
            transform.position += moveAxis * scrollSpeed;
            offset = (player.transform.position -= transform.position);
        }

        Vector3 playerPositionWithOffset = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, playerPositionWithOffset, lerpspeed * Time.deltaTime);
    }
}
