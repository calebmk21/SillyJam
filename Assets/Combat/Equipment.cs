using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Scriptable Objects/Equipment")]
public class Equipment : ScriptableObject
{
    public enum EquipmentType
    {
        WEAPON, ARMOR, ACCESSORY
    }
    
    
    // identifying stuff
    public string Name;
    public string Description;
    public Sprite Icon;
    public int ID;
    public EquipmentType Type;
    
    // Stats
    public int BonusHP;
    public int BonusMP;
    public int BonusATK;
    public int BonusMAG;
    public int BonusDEF;
    public int BonusSPD;
    public int BonusMDEF;


}
