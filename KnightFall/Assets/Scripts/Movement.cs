using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public Vector3 SpawnWhenBuggedOut;
    public float moveSpeed;

    private Vector3 movement;
    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (transform.position.y <= -5f)
        {
            transform.position = SpawnWhenBuggedOut;
        }

        if (movement != Vector3.zero)
        {
            transform.forward = movement;
        }
    }
}
