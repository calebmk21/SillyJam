using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [Header("Data Container")]
    [SerializeField] CharacterStats BaseStats;
    
    [Header("Combat Info")] 
    // private int MaxHP;
    // private int MaxMP;
    //
    // private int ATK;
    // private int MAG;
    // private int DEF;
    // private int MDEF;
    // private int SPD;

    private int EXP;
    
    // In-battle 
    private int CurrentHP;
    private int CurrentMP;

    [Header("Equipment Info")] 
    [SerializeField]  Equipment Armor;
    [SerializeField]  Equipment Weapon;
    [SerializeField]  Equipment Accessory;

    [Header("Party Info")] 
    public bool inParty = true;

    public void DisplayStatInfo()
    {
        
        Debug.Log(BaseStats.HP);
        Debug.Log(BaseStats.ATK);
        Debug.Log(BaseStats.DEF);
        Debug.Log(BaseStats.MAG);
        Debug.Log(BaseStats.MDEF);
        Debug.Log(BaseStats.SPD);
        
    }

    public void LevelUp()
    {
        
    }
    
}

