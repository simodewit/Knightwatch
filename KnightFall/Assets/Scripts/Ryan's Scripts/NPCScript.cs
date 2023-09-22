using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Jobs;

public class NPCScript : MonoBehaviour
{

    public float health = 10;
    
    public Vector3 Position;
    public Transform target;
    public GameObject wall;
    public NavMeshAgent agent;
    public int areaMask;
    public NavMeshPath path;
    public RaycastHit hit;
    public WallScript walls;





    void Start()
    {
        path = new NavMeshPath();


    }


    void Update()
    {
        //agent.destination = castle.position;

        Physics.Raycast(transform.position, target.position,out hit, 10000000f, areaMask);
        Debug.DrawLine(transform.position, target.position);

        wall = hit.transform.gameObject;


        //wall.GetComponent<NavMeshObstacle>().carving = false;

            
        NavMesh.CalculatePath(transform.position, target.transform.position, areaMask, path);
        agent.SetPath(path);
        if(agent.hasPath == false)
        {
            agent.SetDestination(wall.transform.position);
        }
        else
        {
            agent.SetPath(path);
        }
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
        

        
    }
}
