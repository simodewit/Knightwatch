using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Jobs;

public class NPCScript : MonoBehaviour
{
    public Vector3 startPosition;
    public Transform wall;
    public Transform castle;
    public NavMeshAgent agent;
    public int areaMask;
    public NavMeshPath path;


    void Start()
    {
        path = new NavMeshPath();
    }

    private void chase()
    { 

        
    }

    void Update()
    {
        NavMesh.CalculatePath(startPosition, castle.position, areaMask, path);
        agent.SetPath(path);
    }





    public float hp;
    public void DoDamage(float damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
