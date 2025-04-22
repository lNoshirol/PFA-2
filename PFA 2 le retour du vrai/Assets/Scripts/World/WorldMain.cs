using UnityEngine;
using System.Collections.Generic;

public class WorldMain : MonoBehaviour
{

    public static WorldMain Instance { get; private set; }
    public GameObject mapParent;

    public List<GameObject> mapList = new List<GameObject> ();

    public int currentRoomId = 0;

    public MapBounds mapBounds { get; private set; }



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        mapBounds = GetComponent<MapBounds>();
    }


    private void Start()
    {
        HideOtherMaps(currentRoomId);
    }

    void HideOtherMaps(int currentRoom)
    {
        foreach(Transform child in mapParent.transform)
        {
            if (child == mapParent.transform.GetChild(currentRoom))
            {
            }
            else child.gameObject.SetActive(false);

        }
    }

    public void SwitchRoom(int roomIdToLoad)
    {
        HideOtherMaps(roomIdToLoad);
    }
}