using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWorker : MonoBehaviour
{
    [Header("conditions")]
    public float woodNeeded;
    public float stoneNeeded;
    public float metalNeeded;
    public float coinsNeeded;
    public int maxWorkers;

    [Header("scripts and gameobjects")]
    public GameObject spawnPlace;
    public GameObject worker;
    public CounterForMaterials counterForMaterials;
    public IngameUI uiScript;

    private GameObject currentWorker;
    public List<GameObject> workerList;

    public void Click()
    {
        if(workerList.Count >= maxWorkers)
            return;

        if (counterForMaterials.woodAmount < woodNeeded)
            return;

        if (counterForMaterials.stoneAmount < stoneNeeded)
            return;

        if (counterForMaterials.metalAmount < metalNeeded)
            return;

        if (counterForMaterials.coinsAmount < coinsNeeded)
            return;

        counterForMaterials.woodAmount -= woodNeeded;
        counterForMaterials.stoneAmount -= stoneNeeded;
        counterForMaterials.metalAmount -= metalNeeded;
        counterForMaterials.coinsAmount -= coinsNeeded;

        uiScript.PanelIn();
        currentWorker = Instantiate(worker, spawnPlace.transform.position, new Quaternion(0,0,0,0));
        workerList.Add(currentWorker);
    }
}
