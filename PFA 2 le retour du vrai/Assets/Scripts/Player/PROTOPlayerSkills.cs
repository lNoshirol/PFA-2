using UnityEngine;

public class PROTOPlayerSkills : MonoBehaviour
{
    private SimpleDash dash = new();
    
    public GameObject prefab;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            dash.Activate();
        }
    }

    public void UseSkill()
    {
        dash.Activate();
    }
}
