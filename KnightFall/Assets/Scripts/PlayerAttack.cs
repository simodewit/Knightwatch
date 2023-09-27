using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using static UnityEngine.InputSystem.InputAction;

public class PlayerAttack : MonoBehaviour
{
    public string enemieTagName;
    public float damage;
    public Vector3 boxOffset;
    public Vector3 colliderBoxSize;
    public GameObject empty;

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

    public void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + boxOffset, colliderBoxSize);
    }
}
