using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.InputSystem.InputAction;

public class MouseInputForSelecting : MonoBehaviour
{
    public InputMaster input;
    public InputAction move;
    public Vector2 mousePosition;
    public bool selected;
    public GameObject worker;
    public GameObject camera;

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

    public void Clicks(CallbackContext c)
    {
        //if(c.)
        print("clicks");
        mousePosition = move.ReadValue<Vector2>();

        Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100000))
        {
            if(selected == true)
            {
                print("gets position");
                worker.GetComponent<NavMeshAgent>().destination = hit.point;
                selected = false;
            }
            else
            {
                print("selects player");
                if (hit.transform.tag == "Worker")
                {
                    //select worker
                    selected = true;
                    worker = hit.transform.gameObject;
                }
            }
        }
    }
}
