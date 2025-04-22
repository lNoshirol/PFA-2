using Unity.Cinemachine;
using UnityEngine;

public class MapBounds : MonoBehaviour
{

    [SerializeField] private GameObject mapList;
    [SerializeField] private CinemachineCamera cinemachine;
    [SerializeField] private float boundAmount;
    CinemachineConfiner3D confiner;
    private GameObject groundToCopy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        confiner = cinemachine.GetComponent<CinemachineConfiner3D>();
        //FindActiveMap();
        //CreateMapBounds();
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
        BoxCollider bc = groundToCopy.AddComponent<BoxCollider>();
        //bc.isTrigger = true;
        bc.center = new Vector3(0, 0, 0);
        bc.size = new Vector3(boundAmount, 10, 0);
        confiner.BoundingVolume = bc;


    }



}
