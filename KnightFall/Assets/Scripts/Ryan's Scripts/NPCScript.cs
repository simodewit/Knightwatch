using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPCScript : MonoBehaviour
{

    public float hp;

    [Header("Wall & Castle finder")]
    public GameObject nearestWall = null;
    public GameObject castle = null;

    public string tagName;
    public string tagNameCastle;
    //public WallScript w;

     float wallDistanceCastle;
     float distanceToWall;
     float distanceToCastle;

    public float eDamage;


    int areamask = 1;

    public NavMeshAgent agent;
    public NavMeshPath path;




    void Start()
    {

        InvokeRepeating("NearWallFinder", 0f, 0.25f);
        path = new NavMeshPath();
    }


    void Update()
    {
        castle = GameObject.FindGameObjectWithTag(tagNameCastle);
        NavMesh.CalculatePath(transform.position, castle.transform.position, areamask, path);
        agent.SetPath(path);
        






    }

    public void DoDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void NearWallFinder()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag(tagName);
        float shortestdistance = Mathf.Infinity;
        
        foreach(GameObject wall in walls)
        {
           distanceToWall = Vector3.Distance(transform.position, wall.transform.position);
           wallDistanceCastle = Vector3.Distance(wall.transform.position, castle.transform.position);
            if(distanceToWall + wallDistanceCastle <= shortestdistance)
            {
                shortestdistance = distanceToWall + wallDistanceCastle;
                nearestWall = wall;
            }

        }

        

    
    
    }



}
