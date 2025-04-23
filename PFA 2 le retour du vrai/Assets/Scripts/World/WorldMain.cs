using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WorldMain : MonoBehaviour
{

    public static WorldMain Instance { get; private set; }

    public List<GameObject> currentRoomSwitchList = new List<GameObject> ();

    [SerializeField] private GameObject roomSwitchers;

    public string currentRoomName;

    public MapBounds mapBounds { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        mapBounds = GetComponent<MapBounds>();
    }

    private void Start()
    {
        currentRoomName = SceneManager.GetActiveScene().name;
    }
    public void SwitchRoom(string roomName, string switcherName, Vector3 spawnPosition)
    {
        SceneManager.LoadScene(roomName);
        PlayerMain.Instance.PlayerMesh.transform.position = spawnPosition;
    }
}