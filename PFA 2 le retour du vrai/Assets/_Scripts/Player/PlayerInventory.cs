using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 100f)]
    public int paintAmount = 100;
    [Header("NO UPDATE IN RUNTIME")]
    [SerializeField]
    DictItem itemType;

    public Dictionary<ItemTypeEnum, bool> ItemDatabase;
    public Dictionary<string, bool> SpellDataBase = new()
    {
        { "Spirale", false},
        { "Circle", false}
    };

    void Start()
    {
        ItemDatabase = itemType.ToDictionary();
    }

    public void AddItemToInventory(ItemTypeEnum type)
    {
        ItemDatabase[type] = true;
        Debug.Log("Item loot : " + type);
    }
}
[Serializable]
public class DictItem
{
    [SerializeField]
    DictItemType[] thisDictItems;

    public Dictionary<ItemTypeEnum, bool> ToDictionary()
    {
        Dictionary<ItemTypeEnum, bool> itemDatabase = new Dictionary<ItemTypeEnum, bool>();
        foreach (var item in thisDictItems)
        {
            itemDatabase.Add(item.type, item.isLooted);
        }
        return itemDatabase;

    }
}
[Serializable]
public class DictItemType
{
    [SerializeField]
    public ItemTypeEnum type;
    [SerializeField]
    public bool isLooted;
}