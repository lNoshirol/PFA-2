using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rbMain;
    private Vector3 moveInput;

    public Vector2 moveVector2;

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
        moveVector2 = context.ReadValue<Vector2>();

        if (moveInput != Vector3.zero)
        {
            PlayerMain.Instance.playerMesh.transform.rotation = Quaternion.LookRotation(moveInput);
        }
    }
}
