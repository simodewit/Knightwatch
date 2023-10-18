using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TowerScript : MonoBehaviour
{
    public Transform target;
    public string tagName = "Enemy";
    public Transform turretRotation;
    GameObject nearestEnemy = null;
    NPCScript npcS;
    public GameObject tower;
    private Vector3 dir;
    private Vector3 rotation;
    public UnityEngine.UI.Button button;

    public CounterForMaterials counterForMaterials;

    void Start()
    {
        InvokeRepeating("TargetUpdate", 0f, 0.125f);
        levels[currentLevel].platform.SetActive(true);
        tower = this.gameObject;
        currentLevel = currentLevel + 1;    
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

        if(nearestEnemy != null && shortestDistance <= levels[currentLevel -1].range)
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
        turretRotation.rotation = Quaternion.Euler(0f,rotation.y, 0f);

        if (levels[currentLevel - 1].fireCountDown <= 0f)
        {
            if (levels[currentLevel - 1].splashDamage == true)
            {
                levels[currentLevel - 1].fireCountDown = 1f * levels[currentLevel].firerate;
                print("splaashh");
                SplashDamage();
            }
            else
            {
                levels[currentLevel - 1].fireCountDown = 1f * levels[currentLevel - 1].firerate;
                Shoot();
            }
        }
        levels[currentLevel - 1].fireCountDown -= Time.deltaTime;

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

    public void SelectTower(GameObject tower)
    {

    }

    void Shoot()
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
       Collider[] enemies =  Physics.OverlapSphere(target.transform.position, levels[currentLevel -1].splashRange);
        float distance;
        foreach(Collider Enemy in enemies)
        {
            distance = Vector3.Distance(target.position, Enemy.transform.position);
           if(Enemy.transform.tag == tagName)
           {
                print("splashdiddamage;");
                Enemy.GetComponent<NPCScript>().DoDamage(levels[currentLevel - 1].damage - distance);  ;
           }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, levels[currentLevel].range);
    }

    public void OnClickLevel()
    {
    }
    public void LevelUp()
    {
        
        
            //if (counterForMaterials.coinsAmount >= levels[currentLevel].costValues[currentLevel].coinCost)
            //    return;

            //if (counterForMaterials.woodAmount >= levels[currentLevel].costValues[currentLevel].woodCost)
            //    return;

            //if (counterForMaterials.stoneAmount >= levels[currentLevel].costValues[currentLevel].stoneCost)
            //    return;

            //if (counterForMaterials.metalAmount >= levels[currentLevel].costValues[currentLevel].metalCost)
            //    return;           
            //counterForMaterials.woodAmount -= levels[currentLevel].costValues[currentLevel].woodCost;
            //counterForMaterials.stoneAmount -= levels[currentLevel].costValues[currentLevel].stoneCost;
            //counterForMaterials.coinsAmount -= levels[currentLevel].costValues[currentLevel].coinCost;
            //counterForMaterials.metalAmount -= levels[currentLevel].costValues[currentLevel].metalCost;


            levels[currentLevel -1].platform.SetActive(false);
            currentLevel += 1;
            levels[nextLevel - 1].platform.SetActive(true);
            nextLevel += 1;
            var pos = transform.position;
            pos.y = levels[currentLevel -1].platformHeight;
            transform.position = pos;
                     
                 
              
           

        
    }

    public Levels[] levels;
    public int currentLevel;
    public int nextLevel;

    public int maxLevel;   
    

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
    
    public costValues[] costValues;
}

[System.Serializable]
public class costValues
{
    public float coinCost;
    public float woodCost;
    public float stoneCost;
    public float metalCost;
}
