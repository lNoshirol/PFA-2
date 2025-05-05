using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> CurrentEnemyList = new();
    public Dictionary<GameObject, bool> WorldEnemyDic = new();

    private void Start()
    {
        
    }

    public void AddEnemiesToListAndDic()
    {
        foreach(Transform child  in transform)
        {
            AddEnemiesToWorldDic(child.gameObject);
            AddCurrentEnemiesInRoom(child.gameObject);
        }
    }

    public void AddEnemiesToWorldDic(GameObject enemy)
    {
        WorldEnemyDic.Add(enemy, true);
    }

    public void AddCurrentEnemiesInRoom(GameObject currentEnemies)
    {
        CurrentEnemyList.Clear();
        CurrentEnemyList.Add(currentEnemies);
    }
}