using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public List<Enemy> Enemies = new List<Enemy>();

    void Awake()
    {
        Instance = this;
    }

    public void RegisterEnemey(Enemy enemy)
    {
        Enemies.Add(enemy);
        UIManager.Instance.CreateHealthBarForEnemy(enemy);
    }
    public void UnRegister(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }
    public List<Enemy> GetEnemiesInRange(Vector3 postion,float range)
    {
        return Enemies.Where(enemy => Vector3.Distance
            (postion, enemy.transform.position) <= range).ToList();
    }
    public void DestroyAllEnemies()
    {
        foreach (Enemy enemy in Enemies)
        {
            Destroy(enemy.gameObject);
        }
        Enemies.Clear();
    }
    
}
