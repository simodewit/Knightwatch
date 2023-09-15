using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerAttack : MonoBehaviour
{
    public RaycastHit hit;
    public float attackDistance;
    public string enemieTagName;
    public float damage;

    public void LeftMouse()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, attackDistance))
        {
            if(hit.transform.tag == enemieTagName)
            {
                hit.transform.GetComponent<NPCScript>().DoDamage(damage);
            }
        }
    }
}
