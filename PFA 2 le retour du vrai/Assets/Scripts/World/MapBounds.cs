using Unity.Cinemachine;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachine;
    [SerializeField] private float boundAmount;
    private CinemachineConfiner3D confiner;
    private GameObject activeMapBounds;

    void Start()
    {
        confiner = cinemachine.GetComponent<CinemachineConfiner3D>();
        FindActiveMapsBounds();
        ApplyMapBounds();
    }

    GameObject FindActiveMapsBounds()
    {
        foreach (Transform child in WorldMain.Instance.mapParent.transform)
        {
            if(child.gameObject.activeSelf == true)
            {
                activeMapBounds = child.transform.GetChild(0).gameObject;
            }
        }
        return activeMapBounds;

    }

    void ApplyMapBounds()
    {
        confiner.BoundingVolume = activeMapBounds.GetComponentAtIndex<BoxCollider>(4);
    }
}
