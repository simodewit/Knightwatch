using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerScript : MonoBehaviour
{
    [Header("Variables")]

    public float range;
    public float damage;
    public float health;
    public float firerate;
    float fireCountDown  = 0f;

    [Header("Unity Dingen")]

    public Transform target;
    public string tagName = "Enemy";
    public Transform turretRotation;
    GameObject nearestEnemy = null;
    NPCScript npcS;
<<<<<<< Updated upstream
=======
    private Vector3 dir;

>>>>>>> Stashed changes

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

        if(nearestEnemy != null && shortestDistance <= range)
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
<<<<<<< Updated upstream
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
=======
        Debug.DrawRay(turretRotation.position, turretRotation.forward * range);

        dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation( dir );
>>>>>>> Stashed changes
        Vector3 rotation = lookRotation.eulerAngles;
        turretRotation.rotation = Quaternion.Euler(0f,rotation.y, 0f);

        if (fireCountDown <= 0f)
        {
            shoot();
            fireCountDown = 1f * firerate;
        }
        fireCountDown -= Time.deltaTime;

        if (target != null)
        {
            return;
        }
    }

    void shoot()
    {
<<<<<<< Updated upstream
        Physics.Raycast(transform.position, nearestEnemy.transform.position, out RaycastHit hit, range);
        if (hit.transform.tag == tagName)
        {
            target.GetComponent<NPCScript>().DoDamage(damage);
        }
=======
        Debug.Log("Shoot");
        RaycastHit hit;

        Physics.Raycast(transform.position, turretRotation.forward, out hit, range);
        
        if (hit.transform.tag == tagName)
        {
            
            target.GetComponent<NPCScript>().hp -= Damage;           
        }
            
            
            
            


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);   
>>>>>>> Stashed changes
    }
}
