using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading.Tasks;

public class WorldMain : MonoBehaviour
{

    public static WorldMain Instance { get; private set; }

    public List<GameObject> RoomSwitchList = new List<GameObject>();

    public GameObject currentRoomSwitcher;

    public string CurrentRoomName;

    public RoomTransition RoomTransition { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        RoomTransition = gameObject.GetComponent<RoomTransition>();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CurrentRoomName = SceneManager.GetActiveScene().name;
    }

    public void CleanSpawnList()
    {
        RoomSwitchList.Clear();
    }

    public GameObject FindCorrectSpawn(string switcherName)
    {
        foreach (GameObject spawn in RoomSwitchList) {
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
        RoomTransition.Fade(1);
        SceneManager.LoadScene(roomName);
        await Task.Delay(10);
        PlayerMain.Instance.transform.position = FindCorrectSpawn(switcherName).transform.GetChild(0).transform.position;
        RoomTransition.Fade(0);
    }

}