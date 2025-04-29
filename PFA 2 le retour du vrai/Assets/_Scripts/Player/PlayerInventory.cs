using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 100f)]
    int paintAmount = 100;
    [Header("NO UPDATE IN RUNTIME")]
    [SerializeField]
    DictItem itemType;

    public Dictionary<ItemType, bool> itemDatabase;
    
    void Start()
    {
        itemDatabase = itemType.ToDictionary();
    }

    public void AddItemToInventory(ItemType type)
    {
        itemDatabase[type] = true;
        Debug.Log("Item loot : " + type);
    }
}
[Serializable]
public class DictItem
{
    [SerializeField]
    DictItemType[] thisDictItems;

    public Dictionary<ItemType, bool> ToDictionary()
    {
        Dictionary<ItemType, bool> itemDatabase = new Dictionary<ItemType, bool>();
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
    public ItemType type;
    [SerializeField]
    public bool isLooted;
}