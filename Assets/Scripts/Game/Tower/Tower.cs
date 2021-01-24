using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Stone, Fire, Ice
}

public class Tower : MonoBehaviour
{
    public float attackPower = 3f;
    public float timeBetweenAttacksInSeconds = 1f;
    public float aggroRadius = 15f;
    public int towerLevel = 1;
    public TowerType type;
    public AudioClip shootSound;
    public Transform towerPieceToAim;
    public Enemy targetEnemy = null;
    private float attackCounter;

    private void SmootlyLookAtTarget(Vector3 target)
    {
        towerPieceToAim.localRotation = UtilityMethods.SmoothlyLook(towerPieceToAim, target);
    }

    protected virtual void AttackEnemy()
    {
        GetComponent<AudioSource>().PlayOneShot(shootSound, .15f);
    }

    public List<Enemy> GetEnemiesInAggroRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach (Enemy enemy in EnemyManager.Instance.Enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= aggroRadius)
            {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }

    public Enemy GetNearestEnemyInRange()
    {
        Enemy nearestEnemey = null;
        float smallestDistance = float.PositiveInfinity;
        foreach (Enemy enemy in GetEnemiesInAggroRange())
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < smallestDistance)
            {
                smallestDistance = Vector3.Distance(transform.position, enemy.transform.position);
                nearestEnemey = enemy;
            }
        }
        return nearestEnemey;
    }

    public virtual void Update()
    {
        attackCounter -= Time.deltaTime;
        if (targetEnemy == null)
        {
            if (towerPieceToAim)
            {
                SmootlyLookAtTarget(towerPieceToAim.transform.position - new Vector3(0, 0, 1));
            }
            if (GetNearestEnemyInRange() != null && Vector3.Distance
                (transform.position, GetNearestEnemyInRange().transform.position) <= aggroRadius)
            {
                targetEnemy = GetNearestEnemyInRange();
            }
        }
        else
        {
            if (towerPieceToAim)
            {
                SmootlyLookAtTarget(targetEnemy.transform.position);
            }
            if (attackCounter <= 0f)
            {
                AttackEnemy();
                attackCounter = timeBetweenAttacksInSeconds;
            }
            if(Vector3.Distance(transform.position,targetEnemy.transform.position) > aggroRadius)
            {
                targetEnemy = null;
            }
        }
    }
    public void LevelUp()
    {
        towerLevel++;
        attackPower *= 2;
        timeBetweenAttacksInSeconds *= 0.7f;
        aggroRadius *= 1.20f;
    }

    public void ShowTowerInfo()
    {
        UIManager.Instance.ShowTowerInfoWindow(this);
    }
}

