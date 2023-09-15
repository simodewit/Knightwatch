using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingFromTowerScript : MonoBehaviour
{
    public GameObject Camera;

    public bool collides = true;
    public Shader red;
    public Shader green;

    public void Start()
    {
        Camera = GameObject.Find("Main Camera");
    }

    public void Update()
    {
        if(Camera.GetComponent<TowerPlacement>().inBuildingPhase == true)
        {
            if(collides == true)
            {
                GetComponent<Material>().shader = red;
            }
            else if(collides == false)
            {
                GetComponent<Material>().shader = green;
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        collides = true;
    }

    public void OnCollisionExit(Collision collision)
    {
        collides = false;
    }
}
