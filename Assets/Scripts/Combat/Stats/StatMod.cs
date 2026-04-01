using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMod
{
    //public readonly int Order;
    public readonly float Value;
    public readonly StatType StatType;
    public readonly object Source;
    public StatMod(float value, StatType statType, object source)
    {
        this.Value = value;
        this.Source = source;
        this.StatType = statType;
    }

    // public StatMod(float value, StatType statype) : this(value, statype, 1, null) { }
}
