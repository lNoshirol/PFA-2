using System.Threading.Tasks;
using UnityEngine;

public class SimpleDash : SkillParentClass
{
    public async override void Activate()
    {
        float timer = 5f;
        Debug.Log("dash");
        Dash(PlayerMain.Instance.transform.forward, 500);
    }
}
