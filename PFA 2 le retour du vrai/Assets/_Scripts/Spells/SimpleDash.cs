using UnityEngine;

public class SimpleDash : SkillParentClass
{
    public override void Activate(SkillContext context)
    {
        Dash(context.Rigidbody, PlayerMain.Instance.PlayerMesh.transform.forward, 50);
    }
}
