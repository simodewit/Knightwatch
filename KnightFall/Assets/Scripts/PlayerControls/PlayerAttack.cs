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
    [Header("conditions")]
    public string enemieTagName;
    public float damage;
    public Vector3 colliderBoxSize;
    public GameObject empty;
    public GameObject cam;
    public float rotateSpeed;
    public float attackSpeed;

    [Header("do not touch")]
    public InputMaster input;
    public InputAction move;

    private bool attacks;

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

    public void Start()
    {
        empty.transform.localPosition = new Vector3(0, 0, colliderBoxSize.z);
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
            if(attacks == false)
            {
                StartCoroutine(timesIfAttack());
                Collider[] colliders = Physics.OverlapBox(empty.transform.position, colliderBoxSize, transform.rotation);
                foreach (Collider collider in colliders)
                {
                    if (collider.transform.tag == enemieTagName)
                    {
                        collider.GetComponent<NPCwalking>().DoDamage(Mathf.RoundToInt(damage));
                    }
                }
            }
        }
    }
    
    public IEnumerator timesIfAttack()
    {
        attacks = true;
        yield return new WaitForSeconds(attackSpeed);
        attacks = false;
    }
}
