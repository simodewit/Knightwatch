using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceFinder : MonoBehaviour
{
    private float timer;

    public void Start()
    {
        info.woodCounter = GameObject.Find("WoodCounter");
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
                        float newCountWood = float.Parse(info.woodCounter.GetComponent<TextMeshProUGUI>().text) + 1;
                        info.woodCounter.GetComponent<TextMeshProUGUI>().text = newCountWood.ToString();
                        break;

                    case Resource.ResourceType.Stone:
                        float newCountStone = float.Parse(info.stoneCounter.GetComponent<TextMeshProUGUI>().text) + 1;
                        info.stoneCounter.GetComponent<TextMeshProUGUI>().text = newCountStone.ToString();
                        break;

                    case Resource.ResourceType.Metal:
                        float newCountMetal = float.Parse(info.metalCounter.GetComponent<TextMeshProUGUI>().text) + 1;
                        info.metalCounter.GetComponent<TextMeshProUGUI>().text = newCountMetal.ToString();
                        break;
                }
            }
        }
    }
    public Info info;
}

[System.Serializable]

public class Info
{
    public float radius;
    public float waitTimeForNextSpawn;
    public float woodAmountToGoUp;
    public float stoneAmountToGoUp;
    public float metalAmountToGoUp;

    public GameObject woodCounter;
    public GameObject stoneCounter;
    public GameObject metalCounter;
}
