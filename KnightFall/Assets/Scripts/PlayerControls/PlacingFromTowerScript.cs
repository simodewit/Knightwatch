using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingFromTowerScript : MonoBehaviour
{
    public GameObject Camera;

    public bool collides;
    public float radiusForCollision;
    public LayerMask layer;

    public float woodNeeded;
    public float stoneNeeded;
    public float metalNeeded;
    public float coinsNeeded;

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
