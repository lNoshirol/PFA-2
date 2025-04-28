using System.Collections.Generic;
using UnityEngine;

public class GenericPool<T> where T : MonoBehaviour
{
    public Queue<T> ObjectStock { get; private set; } = new Queue<T>();

    public GenericPool(T item, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Stock(GameObject.Instantiate(item));
        }
    }

    /// <summary>
    /// Get an item from the pool and activate it.
    /// </summary>
    /// <returns></returns>
    public T GetObject()
    {
        T item = ObjectStock.Dequeue();
        item.gameObject.SetActive(true);
        return item;
    }

    /// <summary>
    /// Stock an item in the pool and deactivate it.
    /// </summary>
    /// <param name="item"></param>
    public void Stock(T item)
    {
        item.gameObject.SetActive(false);
        ObjectStock.Enqueue(item);
    }
}
