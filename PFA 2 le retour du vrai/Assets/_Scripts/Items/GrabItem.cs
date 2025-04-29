using UnityEngine;
public class GrabItem : MonoBehaviour
{
    public ItemType type;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMain.Instance.Inventory.AddItemToInventory(type);
            gameObject.SetActive(false);
        }
    }
}