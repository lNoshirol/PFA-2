using Unity.Cinemachine;
using UnityEngine;

public class RoomSwitch : MonoBehaviour
{

    [SerializeField] private Vector3 cameraChange;
    [SerializeField] private Vector3 playerChange;
    [SerializeField] private CinemachineCamera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("yoo");
        }
    }
}
