using UnityEngine;
public class GrabItem : MonoBehaviour
{
    public ItemTypeEnum type;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMain.Instance.Inventory.AddItemToInventory(type);
            gameObject.SetActive(false);
            if(type == ItemTypeEnum.Paintbrush)
            {
                ToileMain.Instance.TriggerToile.EnableToileButton();
            }
        }
    }
}