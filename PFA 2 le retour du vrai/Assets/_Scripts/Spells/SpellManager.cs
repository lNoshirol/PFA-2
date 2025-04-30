using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public Dictionary<string, SkillParentClass> Spells { get; private set; } = new() {
        { "Circle", new FireBall() },
        //{ "Triangle"}
    };
    
    // Singleton
    #region Singleton
    private static SpellManager _instance;

    public static SpellManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Spell Manager");
                _instance = go.AddComponent<SpellManager>();
                Debug.Log("<color=#8b59f0>SpellManager</color> instance <color=#58ed7d>created</color>");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            Debug.Log("<color=#8b59f0>SpellManager</color> instance <color=#eb624d>destroyed</color>");
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public void UseSpell(string sigilName)
    {

    }
}
