using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public struct TowerCost
{
    public TowerType TowerType;
    public int Cost;
}
public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;

    public GameObject stoneTowerPrefab;
    public GameObject fireTowerPrefab;
    public GameObject iceTowerPrefab;

    public List<TowerCost> TowerCosts = new List<TowerCost>();

    void Awake()
    {
        Instance = this;
    }

    public void CreateNewTower(GameObject slotToFill, TowerType towerType)
    {
        switch(towerType)
        {
            case TowerType.Stone:
                Instantiate(stoneTowerPrefab, slotToFill.transform.position, Quaternion.identity);
                slotToFill.gameObject.SetActive(false);
                break;
            case TowerType.Fire:
                Instantiate(fireTowerPrefab, slotToFill.transform.position, Quaternion.identity);
                slotToFill.gameObject.SetActive(false);
                break;
            case TowerType.Ice:
                Instantiate(iceTowerPrefab, slotToFill.transform.position, Quaternion.identity);
                slotToFill.gameObject.SetActive(false);
                break;
        }
    }

    public int GetTowerPrice(TowerType towerType)
    {
        return (from towerCost in TowerCosts
                where towerCost.TowerType == towerType
                select towerCost.Cost).FirstOrDefault();
    }

}
