using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _topSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _velocityPower;
    [SerializeField] private float _friction;

    private Vector3 _moveInput;

    void Update()
    {
        float targetSpeedX = _moveInput.x * _topSpeed;
        float targetSpeedZ = _moveInput.z * _topSpeed;

        float speedDifX = targetSpeedX - PlayerMain.Instance.Rigidbody.linearVelocity.x;
        float speedDifZ = targetSpeedZ - PlayerMain.Instance.Rigidbody.linearVelocity.z;

        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? _acceleration : _deceleration;
        float accelRateY = (Mathf.Abs(targetSpeedZ) > 0.01f) ? _acceleration : _deceleration;

        float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, _velocityPower) * Mathf.Sign(speedDifX);
        float movementZ = Mathf.Pow(Mathf.Abs(speedDifZ) * accelRateY, _velocityPower) * Mathf.Sign(speedDifZ);

        Vector3 movementForce = Vector3.right * movementX * Time.deltaTime + Vector3.forward * movementZ * Time.deltaTime;

        if (Mathf.Abs(_moveInput.x) < 0.01f)
        {
            float frictionX = Mathf.Min(Mathf.Abs(PlayerMain.Instance.Rigidbody.linearVelocity.x), Mathf.Abs(_friction));
            frictionX *= Mathf.Sign(PlayerMain.Instance.Rigidbody.linearVelocity.x);
            PlayerMain.Instance.Rigidbody.AddForce(Vector3.right * -frictionX);
        }

        if (Mathf.Abs(_moveInput.z) < 0.01f)
        {
            float frictionZ = Mathf.Min(Mathf.Abs(PlayerMain.Instance.Rigidbody.linearVelocity.z), Mathf.Abs(_friction));
            frictionZ *= Mathf.Sign(PlayerMain.Instance.Rigidbody.linearVelocity.z);
            PlayerMain.Instance.Rigidbody.AddForce(Vector3.forward * -frictionZ);
        }

        PlayerMain.Instance.Rigidbody.AddForce(movementForce);
    } 

    public void Move(InputAction.CallbackContext context)
    {
        _moveInput = new Vector3(context.ReadValue<Vector2>().x, 0f, context.ReadValue<Vector2>().y);

        if (_moveInput != Vector3.zero)
        {
            PlayerMain.Instance.PlayerMesh.transform.rotation = Quaternion.LookRotation(_moveInput);
        }
    }
}
