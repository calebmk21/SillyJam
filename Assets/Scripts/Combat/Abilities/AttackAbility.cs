using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AttackAbility : Ability
{
    public AttackAbility(AbilityData data, GameObject source, List<StatusEffectData> statusEffectDataList) : base(data, source, statusEffectDataList) { }

    public virtual void Trigger(CombatUnit user, CombatUnit target, out int rawDamage)
    {
        AttackAbilityData data = abilityData as AttackAbilityData;
        // can add TriggerEffect method to add vfx
        // data.TriggerEffect(user, target);

        rawDamage = data.DamageFunc(user, target);
        if (statusEffects == null) return;
        foreach (StatusEffect status in statusEffects)
        {
            target.AddStatusEffect(status);
        }
    }

}
