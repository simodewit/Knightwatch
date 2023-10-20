using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingFromTowerScript : MonoBehaviour
{
    [Header("conditions")]
    public float radiusForCollision;
    public LayerMask layer;
    public float woodNeeded;
    public float stoneNeeded;
    public float metalNeeded;
    public float coinsNeeded;
    public Material[] materials;

    [Header("do not touch")]
    public bool collides;
    public GameObject Camera;

    public void Start()
    {
        collides = true;
        Camera = GameObject.Find("Main Camera");
    }

    public void Update()
    {
        collides = Physics.CheckSphere(transform.position, radiusForCollision, layer);

        if(Camera.GetComponent<TowerPlacement>().inBuildingPhase == true)
        {
            if(collides == false)
            {
                GetComponent<Renderer>().material.SetFloat("_Index", 2);
            }
            else if(collides == true)
            {
                GetComponent<Renderer>().material.SetFloat("_Index", 1);
            }
        }
    }

    public void IsPlaced()
    {
        GetComponent<Renderer>().material.SetFloat("_Index", 0);
    }
}
