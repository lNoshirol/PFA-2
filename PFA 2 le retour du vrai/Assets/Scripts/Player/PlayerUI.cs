using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthbarSprite;
    public void UpdatePlayerHealthUI()
    {
        healthbarSprite.fillAmount = (float)PlayerMain.Instance.Health.playerActualHealth / PlayerMain.Instance.Health.playerBaseHealth;
    }

}
