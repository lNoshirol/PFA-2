using UnityEngine;

public class PROTOPlayerSkills : MonoBehaviour
{
    private FireBall _testSkill = new();
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _testSkill.Activate();
        }
    }

    public void UseSkill()
    {
        _testSkill.Activate();
    }
}
