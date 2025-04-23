using Unity.Cinemachine;
using UnityEngine;

public class RoomSwitch : MonoBehaviour
{
    [SerializeField] private RoomSwitcherDATA roomSwitcherData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("yoo");
            WorldMain.Instance.SwitchRoom(roomSwitcherData.targetSceneName, roomSwitcherData.targetSwitcherID);
        }
    }
}
