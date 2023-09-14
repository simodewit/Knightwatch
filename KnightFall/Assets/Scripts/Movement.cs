using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    private Vector3 lookRotation; 
    private Rigidbody rb;
    private InputMaster input;
    private InputAction move;
    private Vector3 movement;

    private void Awake()
    {
        input = new InputMaster();
    }

    public void Start()
    {
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
        lookRotation = new Vector3 (movement.x, 0, movement.z);

        if (lookRotation != Vector3.zero)
        {
            Quaternion look = Quaternion.LookRotation(lookRotation, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, look, rotateSpeed);
        }
    }

    public void FixedUpdate()
    {
        movement.x += move.ReadValue<Vector2>().x;
        movement.z -= move.ReadValue<Vector2>().y;

        rb.velocity = movement * moveSpeed;
    }
}
