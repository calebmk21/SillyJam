using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectData", menuName = "Scriptable Objects/StatusEffectData")]
public class StatusEffectData : ScriptableObject
{
    public int id;
    public string name;
    public string description;
    public int turnDuration;
    public bool isDurationExtendable;
    
}
