using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerhealth : MonoBehaviour
{
    public float hp;
    public float maxhp;

    public void Start()
    {
        hp = maxhp;
    }
    public void DoDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
