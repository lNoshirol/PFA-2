using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rbMain;
    private Vector3 moveInput;

    void Start()
    {
        rbMain = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rbMain.linearVelocity = moveInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = new Vector3(context.ReadValue<Vector2>().x, 0f, context.ReadValue<Vector2>().y);

        if (moveInput != Vector3.zero)
        {
            PlayerMain.Instance.playerMesh.transform.rotation = Quaternion.LookRotation(moveInput);
        }
    }
}
