using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private CanvasGroup worldCanvasGroup;


    private void Start()
    {
        
    }
    public void Fade(int value)
    {
        worldCanvasGroup.alpha = value;
    }

}
