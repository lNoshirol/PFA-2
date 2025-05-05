using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{

    [SerializeField] Canvas enemyCanvas;
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private EnemiesMain enemiesMain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        enemyCanvas.transform.rotation = Quaternion.Euler(45, 0, 0);
    }

    public void UpdateEnemyHealthUI()
    {
        healthbarSprite.fillAmount = (float)enemiesMain.Health.enemyCurrentHealth / enemiesMain.Health.enemyMaxHealth;
    }
}
