using System.Collections;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using UnityEngine;

public enum StatType { MaxHealth, Attack, Defense, Magic, MagicDefense, Speed, Critical, Evasion}


[System.Serializable]
public class CharacterStat
{
    public int baseValue;
    public readonly StatType type;
    public bool _isModified = false;
    private int _lastValue;
    private readonly List<StatMod> _statMods;
    public readonly ReadOnlyCollection<StatMod> StatModifiers; 
    [SerializeField] private int _value;
    public int Value
    {
        get
        {
            if (_isModified || _lastValue != baseValue)
            {
                _value = CalculateFinalValue();
            }
            return _value;
        }
    }

    public CharacterStat()
    {
        _statMods = new List<StatMod>();
        StatModifiers = new ReadOnlyCollection<StatMod>(_statMods);
    }

    public CharacterStat(int baseVal, StatType type) : this()
    {
        this.baseValue = baseVal;
        this.type = type;
    }

    public CharacterStat(StatType statType) : this(0, statType) {}
    

    public void AddModifier(StatMod mod)
    {
        _isModified = true;
        _statMods.Add(mod);
    }

    public bool RemoveModifier(StatMod mod)
    {
        if (_statMods.Remove(mod))
        {
            _isModified = true;
            return true;
        }

        return false;
    }

    public bool RemoveAllModifiers(GameObject source)
    {
        bool didRemove = false;
        for(int i = _statMods.Count - 1; i >= 0; i--)
        {
            if(_statMods[i].Source == source)
            {
                _isModified = true;
                didRemove = true;
                _statMods.RemoveAt(i);
            }
        }
        return didRemove;
    }
    
    // private int CompareModifierOrder(StatMod a, StatMod b)
    // {
    //     if(a.Order < b.Order)
    //     {
    //         return -1;
    //     }
    //     else if(a.Order > b.Order)
    //     {
    //         return 1;
    //     }
    //
    //     return 0;
    // }

    public int CalculateFinalValue()
    {
        float finalValue = baseValue;
        foreach (StatMod mod in _statMods)
        {
            finalValue *= mod.Value;
        }
        int rounded = (int)Math.Round(finalValue, 0);
        
        return rounded;
    }
    

    
    

}