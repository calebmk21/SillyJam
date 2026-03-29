using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "AttackAbilityData", menuName = "Scriptable Objects/AttackAbilityData")]
public class AttackAbilityData : AbilityData
{

    public delegate int del(CombatUnit user, CombatUnit target);
    public del DamageFunc = delegate(CombatUnit user, CombatUnit target)
    {
        int userAtk = user.GetAttack();
        int targetDef = target.GetDefense();

        return userAtk * 2 - targetDef;
    };


    // for VFX, currently unfinished
    public void TriggerEffect(CombatUnit user, CombatUnit target)
    {
        
    }


    public override Ability Initialize(GameObject source)
    {
        return new AttackAbility(this, source, statusEffectDataList);
    }
}
