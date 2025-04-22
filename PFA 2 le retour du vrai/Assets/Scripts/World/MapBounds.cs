using UnityEngine;

public class MapBounds : MonoBehaviour
{

    [SerializeField] private GameObject mapList;
    public GameObject groundToCopy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindActiveMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject FindActiveMap()
    {
        foreach (Transform child in mapList.transform)
        {
            if(child.gameObject.activeSelf == true)
            {
                groundToCopy = child.GetChild(0).gameObject;
                
            }
        }
        return groundToCopy;
    }

    void CreateMapBounds()
    {
        BoxCollider bc = gameObject.AddComponent<BoxCollider>();
    }



}
