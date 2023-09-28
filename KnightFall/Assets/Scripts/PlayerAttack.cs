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

        Vector2 mousePosition = move.ReadValue<Vector2>();
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    public void InputInteraction(CallbackContext c)
    {
        if (c.started)
        {
            print("triggers function");
            Collider[] colliders = Physics.OverlapBox(empty.transform.position, colliderBoxSize, transform.rotation);
            print("finds colliders");
            foreach (Collider collider in colliders)
            {
                if (collider.transform.tag == enemieTagName)
                {
                    print("does damage");
                    collider.GetComponent<NPCScript>().DoDamage(damage);
                }
            }
        }
    }
}
