using System.Threading.Tasks;
using UnityEngine;

public class SimpleDash : SkillParentClass
{
    public async override void Activate()
    {
        float timer = 5f;
        Dash(PlayerMain.Instance.Move.moveVector2, 25);


    }
}
