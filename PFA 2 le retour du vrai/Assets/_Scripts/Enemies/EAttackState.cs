using UnityEngine;

public class EAttackState : EnemiesState
{

    public float attackCooldown;
    public bool alreadyAttack;
    public override void OnEnter()
    {
        EnemiesMain.mat.color = Color.red;
    }

    public override void Do()
    {
        if (!alreadyAttack) {

            transform.LookAt(EnemiesMain.player);
            ///TEMP 
            Rigidbody rb = Instantiate(EnemiesMain.projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 4f, ForceMode.Impulse);
            alreadyAttack = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
        else if (!EnemiesMain.CheckPlayerInAttackRange()) {
            EnemiesMain.SwitchState(EnemiesMain.EChaseState);
        }
    }

    public override void FixedDo()
    {
    }
    public override void OnExit()
    {
    }

    private void ResetAttack()
    {
        alreadyAttack = false;
    }
}
