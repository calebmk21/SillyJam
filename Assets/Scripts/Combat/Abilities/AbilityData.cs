using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    public int id;
    public string name;
    public int power;
    public GameObject source;

    public List<StatusEffectData> statusEffectDataList;

    public abstract Ability Initialize(GameObject source);

}
