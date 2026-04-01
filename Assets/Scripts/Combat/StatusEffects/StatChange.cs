using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StatChange : StatusEffect
{
    StatChangeData _statChangeData;
    CombatUnit _unit;

    public StatChange(StatChangeData data) : base(data)
    {
        _statChangeData = Data as StatChangeData;
    }
    
    protected override void ApplyEffect(CombatUnit unit)
    {
        _unit = unit;
        _unit.AddStatusEffect(this);
    }
    
    
}