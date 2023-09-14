using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningNPC : MonoBehaviour
{
    public GameObject[] placesToSpawn;
    public LevelDetails levelDetails;
    public float timer;
    public Timer timerScript;

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
                for (int j = 0; j < levelDetails.spawnDescriptions[i].enemiesToSpawn; j++)
                {
                    int indexForEnemyType = Random.Range(0, levelDetails.spawnDescriptions[i].enemies.Length);
                    GameObject enemy = Instantiate(levelDetails.spawnDescriptions[i].enemies[indexForEnemyType]);
                    int indexForPlaceToSpawn = Random.Range(0, placesToSpawn.Length);
                    enemy.transform.position = placesToSpawn[indexForPlaceToSpawn].transform.position;
                    yield return new WaitForSeconds(levelDetails.spawnDescriptions[i].spawnInterval);
                }

                if (levelDetails.notImportant.hasCompletedSpawning[i] == false)
                {
                    levelDetails.notImportant.hasCompletedSpawning[i] = true;
                }
            }
        }
    }
}
