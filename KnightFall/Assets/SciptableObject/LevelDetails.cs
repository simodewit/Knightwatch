using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 1)]
public class LevelDetails : ScriptableObject
{
    public SpawnDescriptions[] spawnDescriptions;
    public NotImportant notImportant;
}

[System.Serializable]
public class SpawnDescriptions
{
    public int timeToSpawn;
    public int enemiesToSpawn;
    public float spawnInterval;
    public GameObject[] enemies;
}

[System.Serializable]
public class NotImportant
{
    public bool[] hasCompletedSpawning;
}

