using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float playerBaseHealth;
    [SerializeField] private float playerActualHealth;

    private void Start()
    {
        playerActualHealth = playerBaseHealth;
    }

    private void PlayerHealthChange(int healthChangeAmount)
    {
        playerActualHealth =- healthChangeAmount;

        PlayerMain.Instance.ui.UpdatePlayerHealthUI();

        if (playerActualHealth <= 0) {
            PlayerIsDead();
        }
    }

    private void PlayerIsDead()
    {
        Debug.Log("GameOver");
    }
}
