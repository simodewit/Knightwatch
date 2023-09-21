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
    public Material normalTexture;
    public Material yellowCollorShader;
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

    public void InputInteraction(CallbackContext c)
    {
        if(c.started)
        {
            print("klikt button");
            mousePosition = move.ReadValue<Vector2>();

            Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100000))
            {
                print("schiet raycast");
                if (selected == true)
                {
                    worker.GetComponent<Renderer>().material = normalTexture;
                    worker.GetComponent<NavMeshAgent>().destination = hit.point;
                    selected = false;
                }
                else
                {
                    print(hit.transform.tag);
                    if (hit.transform.tag == "Worker")
                    {
                        print("voert actie uit");
                        selected = true;
                        worker = hit.transform.gameObject;
                        worker.GetComponent<Renderer>().material = yellowCollorShader;
                    }
                }
            }
        }
    }
}
