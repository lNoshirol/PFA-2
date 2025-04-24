using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int playerBaseHealth;
    [SerializeField] public int playerActualHealth;

    private void Start()
    {
        PlayerMain.Instance.UI.UpdatePlayerHealthUI();
    }

    public void PlayerHealthChange(int healthChangeAmount)
    {
        playerActualHealth -= healthChangeAmount;

        PlayerMain.Instance.UI.UpdatePlayerHealthUI();

        if (playerActualHealth <= 0) {
            playerActualHealth = 0;
            PlayerIsDead();
        }
    }

    private void PlayerIsDead()
    {
        Debug.Log("GameOver");
    }
}
