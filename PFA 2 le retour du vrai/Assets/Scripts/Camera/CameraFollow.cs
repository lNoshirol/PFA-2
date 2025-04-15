using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform player;
    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
