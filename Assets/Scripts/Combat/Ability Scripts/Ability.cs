using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class Ability
{
    public int id;
    public string name;
    public int power;
    public GameObject source;
    protected readonly AbilityData abilityData;
    protected readonly List<StatusEffect> statusEffects;
    protected readonly List<StatusEffectData> statusEffectDataList;

    public Ability(AbilityData data, GameObject Source, List<StatusEffectData> statusEffDat)
    {
        id = data.id;
        name = data.name;
        power = data.power;
        source = Source;
        statusEffectDataList = statusEffDat;
        statusEffects = new List<StatusEffect>();
    }

    public Ability(AbilityData data, GameObject source) : this( data, source, null) {}

    protected void InitializeStatusEffect()
    {
        if (statusEffectDataList != null)
        {
            foreach (StatusEffectData data in statusEffectDataList)
            {
                StatusEffect statusEffect = data.Initialize();
                statusEffects.Add(statusEffect);
            }
        }
    }


    






}