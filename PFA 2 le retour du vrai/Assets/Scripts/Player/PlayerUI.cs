using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private float fadeSpeed;
    public void UpdatePlayerHealthUI()
    {
        healthbarSprite.fillAmount = (float)PlayerMain.Instance.Health.playerActualHealth / PlayerMain.Instance.Health.playerBaseHealth;
    }
    private void Start()
    {
        Fade(0);
    }
    public void Fade(int value)
    {
        blackScreen.DOFade(value, fadeSpeed).SetEase(Ease.InOutCubic);
    }

    

}
