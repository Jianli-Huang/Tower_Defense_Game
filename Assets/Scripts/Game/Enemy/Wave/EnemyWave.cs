using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyWave 
{
    public int pathIndex;
    public float startSpawnTimeInSeconds;
    public float timeBetweenSpawnsInSeconds = 1f;
    public List<GameObject> listOfEnemies = new List<GameObject>();

}
