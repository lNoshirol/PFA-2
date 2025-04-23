using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading.Tasks;

public class WorldMain : MonoBehaviour
{

    public static WorldMain Instance { get; private set; }

    public List<GameObject> roomSwitchList = new List<GameObject>();

    [SerializeField] public GameObject currentRoomSwitcher;

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

    public void CleanSpawnList()
    {
        roomSwitchList.Clear();
    }

    public GameObject FindCorrectSpawn(string switcherName)
    {
        foreach (GameObject spawn in roomSwitchList) {
            Debug.Log("I search this switch : " + switcherName);
            Debug.Log("Current spawn in list : " + spawn.name);

            if (spawn.name == switcherName)
            {
                currentRoomSwitcher = spawn;
            }
        }
        return currentRoomSwitcher;
        
    }
    public async void SwitchRoom(string roomName, string switcherName)
    {
        SceneManager.LoadScene(roomName);
        await Task.Delay(10);
        PlayerMain.Instance.transform.position = FindCorrectSpawn(switcherName).transform.GetChild(0).transform.position;
    }

}