using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputAction;

public class PlayerAttack : MonoBehaviour
{
    public string enemieTagName;
    public float damage;
    public Vector3 boxOffset;
    public Vector3 colliderBoxSize;
    public GameObject empty;
    public InputMaster input;
    public InputAction move;
    public GameObject cam;
    public float rotateSpeed;

    private void Awake()
    {
        input = new InputMaster();
    }

    private void OnEnable()
    {
        move = input.Movement3rdperson.mouse;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    public void Update()
    {
        Vector2 mouse = move.ReadValue<Vector2>();
        Ray rays = cam.GetComponent<Camera>().ScreenPointToRay(mouse);
        RaycastHit hit;

        if (Physics.Raycast(rays, out hit, Mathf.Infinity))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    public void InputInteraction(CallbackContext c)
    {
        if (c.started)
        {
            Collider[] colliders = Physics.OverlapBox(empty.transform.position, colliderBoxSize, transform.rotation);
            foreach (Collider collider in colliders)
            {
                if (collider.transform.tag == enemieTagName)
                {
                    collider.GetComponent<NPCScript>().DoDamage(damage);
                }
            }
        }
    }
}
