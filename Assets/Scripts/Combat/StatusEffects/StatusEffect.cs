using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class StatusEffect 
{
    public StatusEffectData Data;
    private int _currentTurnDuration;
    public bool isFinished = false;
    protected int effectStacks;
    
    public StatusEffect(StatusEffectData data)
    {
        Data = data;
    }

    public void DecrementTurnDuration()
    {
        if (_currentTurnDuration > 0)
        {
            _currentTurnDuration--;
        }
        else
        {
            isFinished = true;
            End();
        }
    }
    
    public void Start(CombatUnit unit)
    {
        isFinished = false;
        if (_currentTurnDuration == 0)
        {
            ApplyEffect(unit);
        }

        if (_currentTurnDuration == 0 || Data.isDurationExtendable)
        {
            _currentTurnDuration += Data.turnDuration;
        }
        
    }

    protected abstract void ApplyEffect(CombatUnit unit);
    public virtual void End()
    {
        effectStacks = 0;
    }
    
    
    
}
