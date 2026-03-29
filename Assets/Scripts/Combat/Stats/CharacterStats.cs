using System.Collections;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using UnityEngine;

public enum StatType { MaxHealth, Attack, Defense, Magic, MagicDefense, Speed, Critical, Evasion}


[System.Serializable]
public class CharacterStat
{
    public float baseValue;
    public readonly StatType type;
    public bool isModified = false;
    private readonly List<StatMod> _statMods;
    public readonly ReadOnlyCollection<StatMod> StatModifiers; 



}