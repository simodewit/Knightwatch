using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDetector : MonoBehaviour
{

    public List<ResourceValue> resourceValues = new List<ResourceValue>();


    public List<Resource> resources = new List<Resource>();
    public float harvestCooldown = 0.5f;
    private void Start()
    {
        int resourceTypes = 3;
        for (int i = 0; i < resourceTypes; i++)
        {
            resourceValues.Add(new ResourceValue());
        }
        StartCoroutine(Harvest());
    }

    private void Update()
    {
        GetResourceInRange(5);
    }

    public void GetResourceInRange(float range)
    {
        Collider[] colliders;
        colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider possibleResource in colliders)
        {
            if(possibleResource.GetComponent<Resource>() != null)
            {
                bool canAdd = true;
                foreach (Resource resource in resources)
                {
                    if (resource == possibleResource.GetComponent<Resource>()){
                        canAdd = false;
                    }
                }
                if (canAdd)
                {
                    resources.Add(possibleResource.GetComponent<Resource>());
                }
            }
        }
    }

    public IEnumerator Harvest()
    {
        while (true)
        {
            foreach (Resource toHarvest in resources)
            {
                switch (toHarvest.type)
                {
                    case Resource.ResourceType.Stone:
                        //resourceValues[0].value += toHarvest.value;
                        break;
                    case Resource.ResourceType.Wood:
                        //resourceValues[1].value += toHarvest.value;
                        break;
                    case Resource.ResourceType.Metal:
                        //resourceValues[2].value += toHarvest.value;
                        break;
                }
            }
            yield return new WaitForSeconds(harvestCooldown);
        }
    }
}

[System.Serializable]
public class ResourceValue
{
    public string resourceType;
    public float value;
}