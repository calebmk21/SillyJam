using UnityEngine;

[CreateAssetMenu(fileName = "BattleActions", menuName = "Scriptable Objects/BattleActions")]
public class BattleActions : ScriptableObject
{
    public enum StatusEffect
    {
        Burn, 
        DefDown,
        AtkUp,
        None
    }
    
    public enum Category
    {
        Damage,
        Heal,
        Status
    }

    public int ID;
    public string Action;
    public string Description;
    public StatusEffect Status;
    public Category Cat;
    public float Power;
}
