using UnityEngine;

public class PROTOPlayerSkills : MonoBehaviour
{
    private SimpleDash _testSkill = new();
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //SpellManager.Instance.UseSpell("SimpleDash", this.transform);
            SimpleDash power = (SimpleDash)SpellManager.Instance.GetSpell("SimpleDash");
            SkillContext context = new(PlayerMain.Instance.Rigidbody, this.gameObject, PlayerMain.Instance.PlayerMesh.transform.forward, 50);
            if (power != null) power.Activate(context);
        }
    }

    public void UseSkill()
    {
        SkillContext context = new(PlayerMain.Instance.Rigidbody, this.gameObject, PlayerMain.Instance.PlayerMesh.transform.forward, 50);

        _testSkill.Activate(context);
    }
}
