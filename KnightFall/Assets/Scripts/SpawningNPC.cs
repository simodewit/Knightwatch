using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningNPC : MonoBehaviour
{
    public int[] timeToSpawn;
    public int[] enemiesToSpawn;
    public GameObject[] placesToSpawn;
    public GameObject[] enemies; 
    public bool[] hasCompletedSpawning;
    public float timer;
    public Timer timerScript;

    public void Start()
    {
        timerScript = GetComponent<Timer>();
        timerScript.Init();
    }

    public void Update()
    {
        timerScript.DoUpdate();
        timer = timerScript.timer;

        for (int i = 0; i < timeToSpawn.Length; i++)
        {
            if(timeToSpawn[i] >= timer)
            {
                if(hasCompletedSpawning[i] == false)
                {
                    for (int j = 0; j < enemiesToSpawn[i]; j++)
                    {
                        int indexForEnemyType = Random.Range(0, enemies.Length);
                        GameObject enemy = Instantiate(enemies[indexForEnemyType]);
                        int indexForPlaceToSpawn = Random.Range(0, placesToSpawn.Length);
                        enemy.transform.position = placesToSpawn[indexForPlaceToSpawn].transform.position;
                    }
                    hasCompletedSpawning[i] = true;
                }
            }
        }
    }
}
