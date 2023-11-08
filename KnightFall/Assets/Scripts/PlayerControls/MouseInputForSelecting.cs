using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class MouseInputForSelecting : MonoBehaviour
{
    [Header("conditions")]
    public Material normalTexture;
    public Material yellowCollorShader;
    public GameObject camera;

    [Header("do not touch")]
    public Vector2 mousePosition;
    public bool selected;
    public GameObject worker;
    public InputMaster input;
    public InputAction move;

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

    public void InputInteraction(CallbackContext c)
    {
        if(c.started)
        {
            mousePosition = move.ReadValue<Vector2>();

            Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (selected == true)
                {
                    Deselect(hit.point);
                }
                else if (hit.transform.tag == "Worker")
                {
                    Select(hit.transform.gameObject);
                }
                else if (hit.transform.tag == "Tower")
                {
                    TSelect(hit.transform.gameObject);
                }
            }
        }
    }

    public void Select(GameObject worker)
    {
        selected = true;
        this.worker = worker;
        worker.GetComponent<Renderer>().material = yellowCollorShader;
    }

    public void TSelect(GameObject tower)
    {
        selected = true;
        tower.GetComponent<Renderer>().material = yellowCollorShader;
    }

    public void Deselect(Vector3 point)
    {
        worker.GetComponent<Renderer>().material = normalTexture;
        worker.GetComponent<NavMeshAgent>().destination = point;
        selected = false;
    }
}
