using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    public Vector3 lookRotation; 
    public Rigidbody rb;
    public InputMaster input;
    public InputAction move;
    public Vector3 movement;

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

    private void Update()
    {
        movement.x = move.ReadValue<Vector2>().x;
        movement.z = move.ReadValue<Vector2>().y;
        movement.y = 0;

        lookRotation = new Vector3 (movement.x, 0, movement.z);
        lookRotation.Normalize();

        if (lookRotation != Vector3.zero)
        {
            Quaternion look = Quaternion.LookRotation(lookRotation, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, look, rotateSpeed);
        }
    }

    public void FixedUpdate()
    {
        Vector3 i = movement * moveSpeed;
        rb.velocity = i;
    }
}
