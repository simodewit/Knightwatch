using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class NPCwalking : MonoBehaviour
{
    [Header("conditions")]
    public Slider slider;
    public int damage;
    public float distanceToAttack;
    public float attackCooldown;
    public int hp;
    public string[] tags;
    public float distanceToAttackPeople;
    public string playerTag;
    public string workerTag;
    public float distanceToChasePeople;
    public float distanceToAttackCastle;

    [Header("DoNotTouch")]
    public GameObject castle;
    public GameObject[] walls;
    public NavMeshAgent agent;
    public float totalDistance = Mathf.Infinity;
    public GameObject currentWall;
    public float cooldown;
    public float totalHP;
    public float percentageHP;
    public GameObject player;

    private float currentDistance;
    private GameObject personToTarget;

    public void Start()
    {
        castle = GameObject.Find("Castle");
        agent = GetComponent<NavMeshAgent>();

        totalHP = hp;

        player = GameObject.Find("Player");
    }

    public void Update()
    {
        CalculatesWorkerDistance();
        DoesDamage();
        HpUI();
    }

    public void HpUI()
    {
        percentageHP = hp / totalHP;
        slider.value = percentageHP;
    }

    public void DoDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void CalculatesWorkerDistance()
    {
        if(player != null)
        {
            currentDistance = Vector3.Distance(player.transform.position, transform.position);
            personToTarget = player;
        }
        else
        {
            currentDistance = Mathf.Infinity;
        }

        GameObject[] allWorkers;
        allWorkers = GameObject.FindGameObjectsWithTag("Worker");
        foreach (GameObject worker in allWorkers)
        {
            float newDistance;
            newDistance = Vector3.Distance(worker.transform.position, transform.position);
            if (newDistance <= currentDistance)
            {
                currentDistance = newDistance;
                personToTarget = worker;
            }
        }

        if (currentDistance <= distanceToChasePeople)
        {
            agent.destination = personToTarget.transform.position;
        }
        else
        {
            CalculatePath();
        }
    }

    public void CalculatePath()
    {
        foreach (string tag in tags)
        {
            walls = GameObject.FindGameObjectsWithTag(tag);
        }

        foreach (GameObject wall in walls)
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
        cooldown -= Time.deltaTime;

        if (cooldown <= 0f)
        {
            cooldown = attackCooldown;

            if (personToTarget != null && Vector3.Distance(transform.position, personToTarget.transform.position) <= distanceToAttackPeople)
            {
                if(personToTarget.tag == playerTag)
                {
                    personToTarget.GetComponent<Playerhealth>().DoDamage(damage);
                    return;
                }
                if(personToTarget.tag == workerTag)
                {
                    personToTarget.GetComponent<Playerhealth>().DoDamage(damage);
                    return;
                }
            }

            float distanceToCastle = Vector3.Distance(transform.position, castle.transform.position);
            if (distanceToAttackCastle >= distanceToCastle)
            {
                castle.GetComponent<CastleScript>().DoDamage(damage);
            }

            if (currentWall == null)
                return;

            float distance = Vector3.Distance(transform.position, currentWall.transform.position);

            if (distance <= distanceToAttack)
            {
                currentWall.GetComponent<WallInfo>().DoDamage(damage);
            }
        }
    }
}