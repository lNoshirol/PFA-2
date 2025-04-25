using System.Threading.Tasks;
using UnityEngine;

public class SimpleDash : SkillParentClass
{
    public override void Activate()
    {
        Dash(PlayerMain.Instance.PlayerMesh.transform.forward, 50);
    }
}
