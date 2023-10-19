using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TowerScript : MonoBehaviour
{
    public Transform target;
    public string tagName = "Enemy";
    public Transform turretRotation;
    GameObject nearestEnemy = null;
    public GameObject tower;
    public Button button;
    public CounterForMaterials counterForMaterials;

    void Start()
    {
        InvokeRepeating("TargetUpdate", 0f, 0.125f);
        levels[currentLevel].platform.SetActive(true);
        tower = this.gameObject;
    
        nextLevel = currentLevel + 1;
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
        #region shooting stuff
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Debug.DrawRay(turretRotation.position, turretRotation.forward * levels[currentLevel].range);

        dir = target.position - transform.position;
        lookRotation = Quaternion.LookRotation( dir );

        Vector3 rotation = lookRotation.eulerAngles;
        turretRotation.rotation = Quaternion.Euler(0f,rotation.y -90f, 0f);

        if (levels[currentLevel].fireCountDown <= 0f)
        {
            if (levels[currentLevel ].splashDamage == true)
            {
                levels[currentLevel ].fireCountDown = 1f * levels[currentLevel].firerate;
                print("splaashh");
                SplashDamage();
            }
            else
            {
                levels[currentLevel ].fireCountDown = 1f * levels[currentLevel ].firerate;
                Shoot();
            }
        }
        levels[currentLevel ].fireCountDown -= Time.deltaTime;

        if (target != null)
        {
            return;
        }
        #endregion
        
        if(currentLevel == maxLevel)
        {
            button.enabled = false;
        }
        else
        {
            button.enabled=true;
        }   
    }

    void Shoot()
    {
        Debug.Log("Shoot");

        Physics.Raycast(transform.position, turretRotation.forward, out RaycastHit hit, levels[currentLevel].range);
        
        if (hit.transform.tag == tagName)
        {            
            target.GetComponent<NPCScript>().hp -= levels[currentLevel].damage;           
        }
            
    }
            
            
    void SplashDamage()
    {
       Collider[] enemies =  Physics.OverlapSphere(target.transform.position, levels[currentLevel -1].splashRange);
        float distance;
        foreach(Collider enemy in enemies)
        {
           distance = Vector3.Distance(target.position, enemy.transform.position);
           if(enemy.transform.tag == tagName)
           {
                print("splashdiddamage;");
                enemy.GetComponent<NPCScript>().DoDamage(levels[currentLevel].damage - distance);
           }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, levels[currentLevel].range);
    }


    public void LevelUp()
    {
        print(currentLevel);
        if (counterForMaterials.coinsAmount <= levels[currentLevel].coinCost)
            return;
        
        if (counterForMaterials.woodAmount <= levels[currentLevel].woodCost)
            return;

        if (counterForMaterials.stoneAmount <= levels[currentLevel].stoneCost)
            return;

        if (counterForMaterials.metalAmount <= levels[currentLevel].metalCost)
            return;
        
        counterForMaterials.woodAmount -= levels[currentLevel].woodCost;
        counterForMaterials.stoneAmount -= levels[currentLevel].stoneCost;
        counterForMaterials.coinsAmount -= levels[currentLevel].coinCost;
        counterForMaterials.metalAmount -= levels[currentLevel].metalCost;

        levels[currentLevel ].platform.SetActive(false);
        currentLevel += 1;
        levels[nextLevel ].platform.SetActive(true);
        nextLevel += 1;
        Vector3 pos = transform.position;
        pos.y = levels[currentLevel].platformHeight;
        transform.position = pos;
           
        if(currentLevel + 1 == maxLevel)
        {
            button.gameObject.SetActive(false);
        }
    }

    
    public int currentLevel;
    public int nextLevel;

    public int maxLevel;   
    public Levels[] levels;
    

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

    [Header("Level platform")]
    
    public GameObject platform;
    public float platformHeight;

    [Header("Level Cost")]
    public float coinCost;
    public float woodCost;
    public float stoneCost;
    public float metalCost;
}