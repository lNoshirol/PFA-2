using UnityEngine;

public class PlayerMain : MonoBehaviour
{

    public static PlayerMain Instance { get; private set; }

    public PlayerMove move { get; private set; }
    public PlayerInventory inventory { get; private set; }
    public PlayerHealth health { get; private set; }

    public PlayerUI ui { get; private set; }

    public GameObject playerMesh;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        move = GetComponent<PlayerMove>();
        inventory = GetComponent<PlayerInventory>();
        health = GetComponent<PlayerHealth>();
        ui = GetComponent<PlayerUI>();
    }

}
