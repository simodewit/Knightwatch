using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningNPC : MonoBehaviour
{
    public LevelDetails levelDetails;
    public float timer;
    public Timer timerScript;
    public List<GameObject> currentAliveEnemys = new List<GameObject>();

    public void Start()
    {
        timerScript = GetComponent<Timer>();
        timerScript.Init();
    }

    public void Update()
    {
        StartCoroutine(spawner());
    }

    public IEnumerator spawner()
    {
        timerScript.DoUpdate();
        timer = timerScript.timer;

        for (int i = 0; i < levelDetails.spawnDescriptions.Length; i++)
        {
            if (levelDetails.spawnDescriptions[i].timeToSpawn >= timer)
            {
                if (levelDetails.notImportant.hasCompletedSpawning[i] == false)
                {
                    levelDetails.notImportant.hasCompletedSpawning[i] = true;

                    for (int j = 0; j < levelDetails.spawnDescriptions[i].enemiesToSpawn; j++)
                    {
                        int indexForEnemyType = Random.Range(0, levelDetails.spawnDescriptions[i].enemies.Length);
                        GameObject enemy = Instantiate(levelDetails.spawnDescriptions[i].enemies[indexForEnemyType]);
                        int indexForPlaceToSpawn = Random.Range(0, spawnPlaces[i].placesToSpawn.Length);
                        enemy.transform.position = spawnPlaces[i].placesToSpawn[indexForPlaceToSpawn].transform.position;
                        currentAliveEnemys.Add(enemy);
                        yield return new WaitForSeconds(levelDetails.spawnDescriptions[i].spawnInterval);
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
}
