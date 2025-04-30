using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public Dictionary<SpellParameter, SkillParentClass> Spells { get; private set; } = new() {
        { new("Circle", "#E50037"), new FireBall() },
        { new("Spirale", "#0430A2"), new SimpleDash() }
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

    public void UseSpell(SpellParameter spellParameter)
    {
        // On ne lance pas le sort si on ne l'a pas obtenu dans l'inventaire
        if (!PlayerMain.Instance.Inventory.SpellDataBase[spellParameter.shape]) return;
        Spells[spellParameter].Activate();
    }
}

public class SpellParameter
{
    public string shape;
    public string paintColor;

    public SpellParameter(string shape, string paintColor)
    {
        this.shape = shape;
        this.paintColor = paintColor;
    }

    public static SpellParameter ToSpellParameter(string shape, string color)
    {
        SpellParameter spellParam = new(shape, color);
        return spellParam;
    }
}