using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileDatas _projectileDatas;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = TryGetComponent(out Rigidbody rb) ? rb : null;
        print(_rb);
        print(_projectileDatas.Speed);
    }

    public void Launch()
    {
        _rb.AddForce(Vector3.forward * _projectileDatas.Speed, ForceMode.Impulse);
    }
}
