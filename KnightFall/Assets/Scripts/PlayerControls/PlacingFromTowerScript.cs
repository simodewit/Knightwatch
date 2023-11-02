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

    [Header("red normal and green highlight")]
    public Material greenMaterial;
    public Material redMaterial;
    public Material normalMaterial;

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
        collides = Physics.CheckBox(transform.position, transform.localScale * .5f, Quaternion.identity, layer);

        if(Camera.GetComponent<TowerPlacement>().inBuildingPhase == true)
        {
            if(collides == false)
            {
                GetComponent<Renderer>().material = greenMaterial;
            }
            else if(collides == true)
            {
                GetComponent<Renderer>().material = redMaterial;
            }
        }
    }

    public void IsPlaced()
    {
        GetComponent<Renderer>().material = normalMaterial;
        GetComponent<Collider>().enabled = true;
    }
}
