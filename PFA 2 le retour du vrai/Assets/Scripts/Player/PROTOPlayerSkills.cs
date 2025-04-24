using UnityEngine;

public class PROTOPlayerSkills : MonoBehaviour
{
    private SimpleDash dash = new();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            dash.Activate();
        }
    }
}
