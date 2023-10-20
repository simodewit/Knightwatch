using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("conditions")]
    public float moveSpeed;
    public float rotateSpeed;

    [Header("do not touch")]
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
    }

    public void FixedUpdate()
    {
        Vector3 i = movement * moveSpeed;
        i.y = rb.velocity.y;

        rb.velocity = i;
    }
}
