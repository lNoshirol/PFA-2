using UnityEngine;

public class PROTOPlayerSkills : MonoBehaviour
{
    private SimpleDash dash = new();
    
    public GameObject prefab;
    private GameObject _previousProj;
    private Pool _pool;

    private void Start()
    {
        _pool = new(prefab, 5);    
    }

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

    public void ProtoGetFromPool()
    {
        GameObject current = _pool.GetObject();
        print($"Current projectile : {current}, previous : {_previousProj}. Similar ? {current == _previousProj}.");
        _previousProj = current;
    }
}
