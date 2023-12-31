using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceFinder : MonoBehaviour
{
    [Header("scripts")]
    public CounterForMaterials counterForMaterials;
    public Info info;
    public Animation wood;
    
    private float timer;

    public void Start()
    {
        counterForMaterials = GameObject.Find("MainCanvas").GetComponent<CounterForMaterials>();
    }

    public void Update()
    {
        TimerFunction();
    }

    public void TimerFunction()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = info.waitTimeForNextSpawn;
            GivesResource();
        }
    }

    public void GivesResource()
    {
        Collider[] colliders;
        colliders = Physics.OverlapSphere(transform.position, info.radius);

        foreach (Collider hitCollider in colliders)
        {
            if(hitCollider.GetComponent<Resource>() != null)
            {
                switch (hitCollider.GetComponent<Resource>().type)
                {
                    case Resource.ResourceType.Wood:
                        counterForMaterials.woodAmount += 1;
                        break;

                    case Resource.ResourceType.Stone:
                        counterForMaterials.stoneAmount += 1;
                        break;

                    case Resource.ResourceType.Metal:
                        counterForMaterials.metalAmount += 1;
                        break;
                }
            }
        }
    }
    
}

[System.Serializable]

public class Info
{
    public float radius;
    public float waitTimeForNextSpawn;
    public float woodAmountToGoUp;
    public float stoneAmountToGoUp;
    public float metalAmountToGoUp;
}
