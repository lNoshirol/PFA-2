using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public List<ObjectAmount> ProjectileList = new();

    public Dictionary<string, Pool> ProjectilePools = new();
    
    // Singleton
    #region Singleton
    private static ProjectileManager _instance;

    public static ProjectileManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Projectile Manager");
                _instance = go.AddComponent<ProjectileManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    private void Start()
    {
        foreach (ObjectAmount duo in ProjectileList)
        {
            Pool newPool = new(duo.ObjectPrefab, duo.Amount);
            ProjectilePools.Add(duo.ObjectPrefab.name, newPool);
        }
    }
}
