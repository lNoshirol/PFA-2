using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{

    public float baseDamageAmount = 20;
    [SerializeField] private float animationDuration = 1;

    [SerializeField] private bool canAttack = true;

    [SerializeField] private GameObject attackArea;

    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int NbOfClicks = 0;
    private float lastClickedTime = 0f;
    private float maxComboDelay = 1f;


    private void Start()
    {
        attackArea.SetActive(false);
    }
    public void BaseAttack(InputAction.CallbackContext context)
    {
        if (canAttack && PlayerMain.Instance.Inventory.ItemDatabase[ItemTypeEnum.Paintbrush])
        {
            //StartCoroutine(AttackDuration());
            OnAttack();
        }
    }

    public void BaseAttackMobile()
    {
        if (canAttack && PlayerMain.Instance.Inventory.ItemDatabase[ItemTypeEnum.Paintbrush])
        {
            //StartCoroutine(AttackDuration());
            OnAttack();
        }
    }

    IEnumerator AttackDuration()
    {
        canAttack = false;
        attackArea.SetActive(true);
        yield return new WaitForSeconds(animationDuration);
        attackArea.SetActive(false);
        canAttack = true;
    }

    public void OnAttack()
    {
        lastClickedTime = Time.time;
        NbOfClicks++;
        if (NbOfClicks == 1 && canAttack == true)
        {
            Debug.Log("PlayerAttack.cs : Je fais le combo 1");
            StartCoroutine(AttackDuration());
        }
        NbOfClicks = Mathf.Clamp(NbOfClicks, 0, 3);

        if (NbOfClicks == 2 && canAttack == true)
        {
            Debug.Log("PlayerAttack.cs : Je fais le Combo 2");
            StartCoroutine(AttackDuration());
        }

        if (NbOfClicks >= 3 && canAttack == true)
        {
            Debug.Log("PlayerAttack.cs : Je fais le Combo 3");
            StartCoroutine(AttackDuration());

            NbOfClicks = 0;
        }

        canAttack = true;
    }
}
