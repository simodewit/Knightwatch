using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public Vector3 offset;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(player.transform.position != transform.position)
        {
            offset.x = transform.position.x - player.transform.position.x;
            offset.z = transform.position.z - (player.transform.position.z -10);
            rb.AddForce(-offset, ForceMode.Force);
        }
    }
}
