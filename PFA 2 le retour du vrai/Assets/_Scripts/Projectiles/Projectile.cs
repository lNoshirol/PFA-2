using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileDatas _projectileDatas;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = TryGetComponent(out Rigidbody rb) ? rb : null;
    }

    /// <summary>
    /// Launch the projectile in the direction of its forward vector, with its Datas speed
    /// </summary>
    public void Launch()
    {
        _rb.AddForce(this.transform.forward * this._projectileDatas.Speed, ForceMode.Impulse);
        // Jouer SFX et VFX OnLaunch
    }
}
