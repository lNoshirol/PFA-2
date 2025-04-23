using UnityEngine;

public class SimpleDash : SkillParentClass
{
    public override void Activate()
    {
        Dash(PlayerMain.Instance.Move.moveVector2, 5);
    }
}
