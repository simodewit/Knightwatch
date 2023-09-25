using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using static UnityEngine.InputSystem.InputAction;

public class PlayerAttack : MonoBehaviour
{
    public RaycastHit hit;
    public float attackDistance;
    public string enemieTagName;
    public float damage;
    public float radius;

    public void InputInteraction(CallbackContext c)
    {
        print("triggeres funtion");
        if (c.started)
        {
            print("finds started button");
            if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, attackDistance))
            {
                print("shoots raycast");
                if (hit.transform.tag == enemieTagName)
                {
                    print("does damage");
                    hit.transform.GetComponent<NPCScript>().DoDamage(damage);
                }
            }
        }
    }
}
