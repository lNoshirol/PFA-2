using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private float fadeSpeed;
    
    public void Fade(int value)
    {
        blackScreen.DOFade(value, fadeSpeed).SetEase(Ease.InOutCubic);
    }

}
