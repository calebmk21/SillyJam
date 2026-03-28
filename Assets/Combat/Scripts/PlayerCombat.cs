using Unity.Mathematics;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [Header("Data Container")]
    [SerializeField] CharacterStats BaseStats;
    [SerializeField] StatGrowths Growths;
    
    [Header("Stat Info")] 
    private int MaxHP;
    private int MaxMP;
    
    private int ATK;
    private int MAG;
    private int DEF;
    private int MDEF;
    private int SPD;

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
    public bool isTheirTurn = false;

    [Header("Status Info")]
    public bool isAlive = true;

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

    #region Stat Modifications

    public void TakeDamage(int damage)
    {
        CurrentHP = math.max(CurrentHP -= damage, 0);
    }

    public void Heal(int healing)
    {
        CurrentHP = math.min(CurrentHP += healing, MaxHP);
    }

    // generic buffing method for increasing stat
    public void Buff()
    {
        
    }
    #endregion
}

