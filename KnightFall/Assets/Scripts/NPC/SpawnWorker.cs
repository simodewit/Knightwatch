using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWorker : MonoBehaviour
{
    public GameObject spawnPlace;
    public GameObject worker;
    public GameObject currentWorker;

    public void Click()
    {
        currentWorker = Instantiate(worker);
        currentWorker.transform.position = spawnPlace.transform.position;
    }
}
