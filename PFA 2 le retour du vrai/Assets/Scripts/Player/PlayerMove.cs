using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 moveInput;

    public Vector2 moveVector2;

    void Update()
    {
        Vector3 movement = moveInput * moveSpeed;
        PlayerMain.Instance.Rigidbody.linearVelocity = new Vector3(movement.x, PlayerMain.Instance.Rigidbody.linearVelocity.y, movement.z);
        PlayerMain.Instance.Rigidbody.linearVelocity = new Vector3(Mathf.Clamp(PlayerMain.Instance.Rigidbody.linearVelocity.x + movement.x, -5, 5), PlayerMain.Instance.Rigidbody.linearVelocity.y, Mathf.Clamp(PlayerMain.Instance.Rigidbody.linearVelocity.z + movement.z, -5, 5));
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = new Vector3(context.ReadValue<Vector2>().x, 0f, context.ReadValue<Vector2>().y);
        moveVector2 = context.ReadValue<Vector2>();

        if (moveInput != Vector3.zero)
        {
            PlayerMain.Instance.PlayerMesh.transform.rotation = Quaternion.LookRotation(moveInput);
        }
    }
}
