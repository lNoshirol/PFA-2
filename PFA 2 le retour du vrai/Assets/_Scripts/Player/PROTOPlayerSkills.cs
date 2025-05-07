using UnityEngine;

public class PROTOPlayerSkills : MonoBehaviour
{
    public void UseSkill()
    {
        SimpleDash power = (SimpleDash)SpellManager.Instance.GetSpell("SimpleDash");
        Debug.Log("ALED");
        SkillContext context = new(PlayerMain.Instance.Rigidbody, this.gameObject, PlayerMain.Instance.PlayerMesh.transform.forward, 50);
        if (power != null) power.Activate(context);
    }
}