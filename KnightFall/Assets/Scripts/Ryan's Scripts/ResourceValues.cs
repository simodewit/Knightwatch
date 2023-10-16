using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceValues : MonoBehaviour
{
    public ResourceFinder value;
    public float coins;
    public float wood;
    public float stone;
    public float metal;


    void Start()
    {
        InvokeRepeating("ValueCounter", 0, 1f);
    }


    public void ValueCounter()
    {
        coins = value.counterInfo.coins;
        wood = value.counterInfo.wood;
        stone = value.counterInfo.stone;
        metal = value.counterInfo.metal;
    }
}
