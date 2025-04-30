using UnityEngine;

public class PROTOPlayerSkills : MonoBehaviour
{
    private SimpleDash _testSkill = new();
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpellManager.Instance.UseSpell(new("Spirale", "0430A2"));
        }
    }

    public void UseSkill()
    {
        _testSkill.Activate();
    }
}
