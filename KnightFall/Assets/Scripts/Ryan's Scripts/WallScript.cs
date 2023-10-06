using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class WallScript : MonoBehaviour
{
    public float health;
    public Vector3 pos;
    public NavMeshObstacle obstacle;
    public float wallcost;
    public NPCScript npcScript;
    public string enemyTag;
    public float delayFactor = 0.1f;

    void Start()
    {
       pos = transform.position;

      
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }   
    }

    private void OnTriggerStay(Collider other)
    {
        print("hit");
        if (other.tag == enemyTag)
        {
            health -= npcScript.eDamage * delayFactor;
        }
    }

}
