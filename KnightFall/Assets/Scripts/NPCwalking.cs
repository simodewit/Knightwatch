using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class NPCwalking : MonoBehaviour
{
    [Header("conditions")]
    public int damage;
    public float distanceToAttack;
    public float attackCooldown;
    public int hp;

    [Header("do not touch")]
    public GameObject castle;
    public GameObject[] walls;
    public NavMeshAgent agent;
    public string[] tags;
    float totalDistance = Mathf.Infinity;
    public GameObject currentWall;
    private float cooldown;

    public void Start()
    {
        castle = GameObject.Find("Castle");
        agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        CalculatePath();
        DoesDamage();
    }

    public void CalculatePath()
    {
        foreach(string tag in tags)
        {
            walls = GameObject.FindGameObjectsWithTag(tag);
        }

        foreach(GameObject wall in walls)
        {
            float distanceNPCToWall = Vector3.Distance(transform.position, wall.transform.position);
            float distanceWallToCastle = Vector3.Distance(wall.transform.position, castle.transform.position);

            if (totalDistance > distanceNPCToWall + distanceWallToCastle + wall.GetComponent<WallInfo>().wallCost)
            {
                totalDistance = distanceWallToCastle + distanceNPCToWall + wall.GetComponent<WallInfo>().wallCost;
                currentWall = wall;
            }
        }

        NavMeshPath pathToCastle = new NavMeshPath();
        agent.CalculatePath(castle.transform.position, pathToCastle);

        if (pathToCastle.status != NavMeshPathStatus.PathPartial)
        {
            agent.SetPath(pathToCastle);
            if (totalDistance < agent.remainingDistance)
            {
                agent.destination = currentWall.transform.position;
            }
            else
            {
                currentWall = null;
                agent.destination = castle.transform.position;
            }
        }
        else
        {
            agent.destination = currentWall.transform.position;
        }
    }

    public void DoesDamage()
    {
        if (currentWall == null)
            return;

        cooldown -= Time.deltaTime;
        float distance = Vector3.Distance(transform.position, currentWall.transform.position);

        if(distance <= distanceToAttack)
        {
            if(cooldown <= 0f)
            {
                currentWall.GetComponent<WallInfo>().DoDamage(damage);
                cooldown = attackCooldown;
            }
        }
    }

    public void DoDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}