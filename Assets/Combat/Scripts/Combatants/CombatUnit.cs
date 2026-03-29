using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CombatUnit : MonoBehaviour
{

    [Header("Data Container")]
    [SerializeField] CharacterStats BaseStats;
    [SerializeField] BattleActions Action1, Action2, Action3, Action4;
    BattleActions[] _actions = new BattleActions[4];
    
    [Header("Stat Info")] 
    private int MaxHP;
    private int MaxMP;
    
    private int ATK;
    private int MAG;
    private int DEF;
    private int MDEF;
    private int SPD;
    
    private bool isPartyMember;
    
    
    // In-battle 
    private int CurrentHP;
    private int CurrentMP;
    private BattleActions.StatusEffect Status; 

    [Header("Equipment Info")] 
    [SerializeField]  Equipment Armor;
    [SerializeField]  Equipment Weapon;
    [SerializeField]  Equipment Accessory;
    
    [Header("Status Info")]
    public bool isAlive = true;



    void Start()
    {
        ATK = BaseStats.ATK;
        MAG = BaseStats.MAG;
        DEF = BaseStats.DEF;
        MDEF = BaseStats.MDEF;
        SPD = BaseStats.SPD;
        isPartyMember = BaseStats.isPartyMember;
        
        _actions[0] = Action1;
        _actions[1] = Action2;
        _actions[2] = Action3;
        _actions[3] = Action4;
        
    }

    #region Combat Actions

    public void DoAction(BattleActions action, CombatUnit target)
    {
        BattleActions.Category moveType =  action.Cat;

        switch (moveType)
        {
            case BattleActions.Category.Damage:
                break;
            case BattleActions.Category.Heal:
                break;
            case BattleActions.Category.Status:
                break;
        }
    }
    
    // Enemy Actions
    public void EnemyAction()
    {
        
    }
    
    #endregion

    #region Getters/Setters
    public bool IsPartyMember()
    {
        return isPartyMember;
    }

    public int GetSpeed()
    {
        return SPD;
    }

    public string GetName()
    {
        return BaseStats.Name;
    }

    public BattleActions[] GetBattleActions()
    {
        return _actions;
    }
    #endregion
    
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
    
    #region Battle Boilerplate

    public void CharacterNudge()
    {
        this.transform.position += new  Vector3(4f, 0, 0);
    }

    public int DamageCalculator(int offense, int power, int defense)
    {
        int damage = offense * power - 2 * defense;
        return damage;
    }
    
    #endregion
    
    #region Debug
    public void DisplayStatInfo()
    {
        Debug.Log(BaseStats.HP);
        Debug.Log(BaseStats.ATK);
        Debug.Log(BaseStats.DEF);
        Debug.Log(BaseStats.MAG);
        Debug.Log(BaseStats.MDEF);
        Debug.Log(BaseStats.SPD);
    }
    #endregion
    
    #region Special Attack Methods

    public int DaggerDance(int atk, int spd, int def, int pow)
    {
        int damage = pow * spd + atk - def;
        return damage;
    }
    
    
    #endregion
    
}

