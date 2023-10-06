using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawningNPC : MonoBehaviour
{
    public string timer;
    public Timer timerScript;
    public List<GameObject> currentAliveEnemys = new List<GameObject>();

    public void Start()
    {
        timerScript = GetComponent<Timer>();
    }

    public void Update()
    {
        StartCoroutine(spawner());
    }

    public IEnumerator spawner()
    {
        timerScript.Init();
        timer = timerScript.text;

        for(int a = 0; a < spawnPlaces.Length; a++)
        {
            for (int i = 0; i < spawnPlaces[a].spawnDescriptions.Length; i++)
            {
                if (spawnPlaces[a].spawnDescriptions[i].timeToSpawn == timer)
                {
                    if (spawnPlaces[a].notImportant.hasCompletedSpawning == false)
                    {
                        spawnPlaces[a].notImportant.hasCompletedSpawning = true;

                        for (int j = 0; j < spawnPlaces[a].spawnDescriptions[i].enemiesToSpawn; j++)
                        {
                            int indexForEnemyType = Random.Range(0, spawnPlaces[a].spawnDescriptions[i].enemies.Length);
                            GameObject enemy = Instantiate(spawnPlaces[a].spawnDescriptions[i].enemies[indexForEnemyType]);
                            int indexForPlaceToSpawn = Random.Range(0, spawnPlaces[i].placesToSpawn.Length);
                            enemy.transform.position = spawnPlaces[i].placesToSpawn[indexForPlaceToSpawn].transform.position;
                            currentAliveEnemys.Add(enemy);
                            yield return new WaitForSeconds(spawnPlaces[a].spawnDescriptions[i].spawnInterval);
                        }
                    }
                }
            }
        }
    }
    public SpawnPlaces[] spawnPlaces;
}

[System.Serializable]
public class SpawnPlaces
{
    public GameObject[] placesToSpawn;
    public SpawnDescriptions[] spawnDescriptions;
    public NotImportant notImportant;
}

[System.Serializable]
public class SpawnDescriptions
{
    public string timeToSpawn;
    public int enemiesToSpawn;
    public float spawnInterval;
    public GameObject[] enemies;
}

[System.Serializable]
public class NotImportant
{
    public bool hasCompletedSpawning;
}
