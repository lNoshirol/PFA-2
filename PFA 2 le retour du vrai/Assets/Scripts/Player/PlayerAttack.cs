using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float baseDamageAmount = 5;
    [SerializeField] private float animationDuration = 1;

    [SerializeField] private bool canAttack = true;

    [SerializeField] private GameObject attackArea;


    private void Start()
    {
        attackArea.SetActive(false);
    }
    public void BaseAttack(InputAction.CallbackContext context)
    {
        if(canAttack) {
            StartCoroutine(AttackDuration());
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
}
