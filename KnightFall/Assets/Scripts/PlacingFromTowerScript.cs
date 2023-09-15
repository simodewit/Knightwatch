using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingFromTowerScript : MonoBehaviour
{
    public GameObject Camera;

    public bool collides;
    public Shader red;
    public Shader green;

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

    public void OnCollisionEnter(Collision collision)
    {
        collides = true;
    }

    public void OnCollisionExit(Collision collision)
    {
        collides = false;
    }
}
