using Unity.Cinemachine;
using UnityEngine;

public class RoomSwitch : MonoBehaviour
{
    [SerializeField] private Vector3 cameraChange;
    [SerializeField] private Vector3 playerChange;
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private int roomIdToLoad;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("yoo");
            other.transform.position += playerChange;
            WorldMain.Instance.SwitchRoom(roomIdToLoad);
        }
    }
}
