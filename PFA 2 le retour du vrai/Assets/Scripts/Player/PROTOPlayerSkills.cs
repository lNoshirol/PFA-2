using UnityEngine;

public class PROTOPlayerSkills : MonoBehaviour
{
    private FireBall dash = new();
    
    public GameObject prefab;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("use KATON FIRE BALL");
            dash.Activate();
        }
    }

    public void UseSkill()
    {
        dash.Activate();
    }
}
