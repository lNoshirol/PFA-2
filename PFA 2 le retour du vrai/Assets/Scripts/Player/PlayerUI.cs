using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private CanvasGroup playerControl;
    [SerializeField] private float fadeSpeed;
    public void UpdatePlayerHealthUI()
    {
        healthbarSprite.fillAmount = (float)PlayerMain.Instance.Health.playerActualHealth / PlayerMain.Instance.Health.playerBaseHealth;
    }
    private void Start()
    {
        Fade(0, blackScreen);
    }
    public void Fade(int value, CanvasGroup group)
    {
        group.DOFade(value, fadeSpeed).SetEase(Ease.InOutCubic);
    }


    public void SwitchRoomUI()
    {
        if (blackScreen.alpha == 0 & playerControl.alpha == 1)
        {
            Fade(1, blackScreen);
            Fade(0, playerControl);
        }
        else {
            Fade(0, blackScreen);
            Fade(1, playerControl);
        }

    }

    

}
