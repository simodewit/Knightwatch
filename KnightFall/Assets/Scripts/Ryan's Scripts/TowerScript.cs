using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerScript : MonoBehaviour
{
    public Transform target;
    public string tagName = "Enemy";
    public Transform turretRotation;
    GameObject nearestEnemy = null;
    NPCScript npcS;

    private Vector3 dir;
    private Vector3 rotation;

    void Start()
    {
        InvokeRepeating("TargetUpdate", 0f, 0.125f);
    }

    private void TargetUpdate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tagName);
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance )
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= levels[currentLevel].range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    void Update()
    {

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Debug.DrawRay(turretRotation.position, turretRotation.forward * levels[currentLevel].range);

        dir = target.position - transform.position;
        lookRotation = Quaternion.LookRotation( dir );

        Vector3 rotation = lookRotation.eulerAngles;
        turretRotation.rotation = Quaternion.Euler(0f,rotation.y, 0f);

        if (levels[currentLevel].fireCountDown <= 0f)
        {
            if (levels[currentLevel].splashDamage == true)
            {
                levels[currentLevel].fireCountDown = 1f * levels[currentLevel].firerate;
                print("splaashh");
                SplashDamage();
            }
            else
            {
                levels[currentLevel].fireCountDown = 1f * levels[currentLevel].firerate;
                shoot();
            }
        }
        levels[currentLevel].fireCountDown -= Time.deltaTime;

        if (target != null)
        {
            return;
        }
    }

    void shoot()
    {
        Physics.Raycast(transform.position, nearestEnemy.transform.position, out RaycastHit hit, levels[currentLevel].range);
        if (hit.transform.tag == tagName)
        {
            target.GetComponent<NPCScript>().DoDamage(levels[currentLevel].damage);
        }

        Debug.Log("Shoot");

        Physics.Raycast(transform.position, turretRotation.forward, out hit, levels[currentLevel].range);
        
        if (hit.transform.tag == tagName)
        {            
            target.GetComponent<NPCScript>().hp -= levels[currentLevel].damage;           
        }
            
    }
            
            
    void SplashDamage()
    {
       Collider[] enemies =  Physics.OverlapSphere(target.transform.position, levels[currentLevel].splashRange);
        float distance;
        foreach(Collider Enemy in enemies)
        {
            distance = Vector3.Distance(target.position, Enemy.transform.position);
           if(Enemy.transform.tag == tagName)
           {
                print("splashdiddamage;");
                Enemy.GetComponent<NPCScript>().DoDamage(levels[currentLevel].damage - distance);  ;
           }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, levels[currentLevel].range);
    }

    public Levels[] levels;
    public int currentLevel;
}

[System.Serializable]
public class Levels
{
    [Header("Ranges")]

    public float range;
    public float minumumRange;
    public float splashRange;

    [Header("Gun Variables")]
    public bool splashDamage;
    public float damage;
    public float health;
    public float firerate;
    public float fireCountDown = 0f;
}


