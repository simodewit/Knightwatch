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
    public float maxDistance;
    public float minDistance;

    [Header("do not touch")]
    public GameObject player;
    public Rigidbody rb;
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
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float Input = move.ReadValue<float>();
        float scrollPosition =  Input / 120;

        if(offset.y >= 27.1 && scrollPosition < 0)
        {
            scrollPosition = 0;
        }

        if(offset.y <= 10.95 && scrollPosition > 0)
        {
            scrollPosition = 0;
        }

        if (scrollPosition > 0)
        {
            offset.y -= 0.85f;
            offset.z += 0.5f;
        }
        if (scrollPosition < 0)
        {
            offset.y += 0.85f;
            offset.z -= 0.5f;
        }

        Vector3 endPosition = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, endPosition, lerpspeed * Time.deltaTime);
    }
}
