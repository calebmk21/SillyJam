using UnityEngine;

[CreateAssetMenu(fileName = "StatDecrease", menuName = "StatusEffect/StatChange")]
public class StatChangeData : StatusEffectData
{
    public float value;
    private StatType statType;
    public StatMod statMod; 

    public override StatusEffect Initialize()
    {
        return new StatChange(this);
    }

    private void OnEnable()
    {
        statMod = new StatMod(value, statType, this);
    }
    
}
