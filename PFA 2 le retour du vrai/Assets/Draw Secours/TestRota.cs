using UnityEngine;

public class TestRota : MonoBehaviour
{
    public Vector3 rotation;
    public GameObject truc;

    private void Start()
    {
        truc.transform.position = Quaternion.Euler(rotation) * transform.position;
    }
}
