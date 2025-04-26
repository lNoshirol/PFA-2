using System.Threading.Tasks;
using UnityEngine;

public class SimpleDash : SkillParentClass
{
    public override void Activate()
    {
        Dash(PlayerMain.Instance.PlayerMesh.transform.forward, 50);
        //Delegate[] functions = { PrintRandomTest, PrintRandomTest, PrintRandomTest };
        //DelayedFunction(functions, 1f);
    }
}
