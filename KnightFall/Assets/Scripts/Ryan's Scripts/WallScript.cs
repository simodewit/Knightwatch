using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class WallScript : MonoBehaviour
{
    public int health;
    public Vector3 pos;
    public NavMeshObstacle obstacle;

    void Start()
    {
       pos = transform.position;
      
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.GetComponent<NavMeshObstacle>().carving == false)
        {

        }
        
    }
    private void OnCollisionStay(Collision collision)
    {
         
    }
}
