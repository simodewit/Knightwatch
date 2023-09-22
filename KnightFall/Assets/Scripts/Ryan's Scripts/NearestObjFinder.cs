using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestObjFinder : MonoBehaviour
{
    public GameObject[] wall;
    public GameObject nearestWall;
    public float distance;
    public float[] distances; 
    public float nearestDistance;


    void Start()
    {
        wall = GameObject.FindGameObjectsWithTag("Wall");
    }

    void Update()
    {



    
    }
}
