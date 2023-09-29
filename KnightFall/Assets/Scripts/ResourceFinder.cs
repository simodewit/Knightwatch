using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceFinder : MonoBehaviour
{
    private float timer;

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
                        float newCountWood = counterInfo.wood + 1;
                        counterInfo.wood = newCountWood;
                        break;

                    case Resource.ResourceType.Stone:
                        float newCountStone = counterInfo.stone + 1;
                        counterInfo.stone = newCountStone;
                        break;

                    case Resource.ResourceType.Metal:
                        float newCountMetal = counterInfo.metal + 1;
                        counterInfo.metal = newCountMetal;
                        break;
                }
            }
        }
    }
    public Info info;
    public InfoForCounters counterInfo;
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
