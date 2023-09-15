using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGame : MonoBehaviour
{
    public SpawningNPC SpawningNPC;
    public Timer timer;
    public LevelDetails levelDetails;
    public GameObject text;

    public void Awake()
    {
        for (int i = 0; i < levelDetails.notImportant.hasCompletedSpawning.Length; i++)
        {
            levelDetails.notImportant.hasCompletedSpawning[i] = false;
        }
    }

    public void Start()
    {
        timer = GetComponent<Timer>();
        SpawningNPC = GetComponent<SpawningNPC>();
    }

    public void Update()
    {
        if(timer.timer <= 0)
        {
            timer.timer = 0;

            if (SpawningNPC.currentAliveEnemys.Count == 0)
            {
                text.SetActive(true);
            }
        }
    }
}
