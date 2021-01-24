using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public List<EnemyWave> enemyWaves = new List<EnemyWave>();

    private float elapsedTime = 0f;

    private EnemyWave activeWave;

    private float spawnCounter = 0f;

    private List<EnemyWave> activedWaves = new List<EnemyWave>();

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        SearchForWave();
        UpdateActivedWave();
    }

    private void SearchForWave()
    {
        foreach (EnemyWave enemyWave in enemyWaves)
        {
            if (!activedWaves.Contains(enemyWave) &&
                enemyWave.startSpawnTimeInSeconds <= elapsedTime)
            {
                activeWave = enemyWave;
                activedWaves.Add(enemyWave);
                spawnCounter = 0f;
                GameManager.Instance.waveNumber++;
                UIManager.Instance.ShowCenterWindow("Wave " + GameManager.Instance.waveNumber);
                break;
            }
        }
    }

    private void UpdateActivedWave()
    {
        if (activeWave != null)
        {
            spawnCounter += Time.deltaTime;

            if (spawnCounter >= activeWave.timeBetweenSpawnsInSeconds)
            {
                spawnCounter = 0;

                if (activeWave.listOfEnemies.Count != 0)
                {
                    GameObject enemy = (GameObject)Instantiate(activeWave.listOfEnemies[0],
                        WayPointManager.Instance.GetSpawnPosition(activeWave.pathIndex),
                        Quaternion.identity);

                    enemy.GetComponent<Enemy>().pathIndex = activeWave.pathIndex;

                    activeWave.listOfEnemies.RemoveAt(0);
                }
                else
                {
                    activeWave = null;
                    if(activedWaves.Count == enemyWaves.Count)
                    {
                        GameManager.Instance.enemySpawningOver = true;
                    }
                }
            }
        }
    }

    public void StopSpawning()
    {
        elapsedTime = 0;
        spawnCounter = 0;
        activeWave = null;
        activedWaves.Clear();
        enabled = false;
    }
}
