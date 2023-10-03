using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInfo : MonoBehaviour
{
    public int hp;
    public int wallCost;

    public void DoDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
