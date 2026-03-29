using JetBrains.Annotations;
using NUnit.Framework.Interfaces;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class CombatUnit : MonoBehaviour
{

    [Header("Data Container")]
    [SerializeField] CharacterStats BaseStats;

    // TODO change later to fit new system
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
    protected Dictionary<StatusEffectData, StatusEffect> statusEffects;


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



    public string GetName()
    {
        return BaseStats.Name;
    }

    // Stats
    public int GetAttack()
    {
        return ATK;
    }

    public int GetDefense()
    {
        return DEF;
    }

    public int GetMagic()
    {
        return MAG;
    }

    public int GetMagicDefense()
    {
        return MDEF;
    }
        public int GetSpeed()
    {
        return SPD;
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

    public void AddStatusEffect(StatusEffect statusEffect)
    {
        Debug.Log("Status effect added: " + statusEffect.Data.name + " on " + this.name);
        if (statusEffects.ContainsKey(statusEffect.Data))
        {
            statusEffects[statusEffect.Data].Start(this);
        }
        else
        {
            statusEffects.Add(statusEffect.Data, statusEffect);
            statusEffects[statusEffect.Data].Start(this);
        }
    }

    public void ModifyStats()
    {
        
    }


    #endregion
    
    #region Battle Boilerplate

    public void CharacterNudge()
    {
        this.transform.position += new  Vector3(4f, 0, 0);
    }

    // public int DamageCalculator(Func<CombatUnit, CombatUnit, Null, Null, Null, int> lambda, CombatUnit target)
    // {
    //     // not sure why it requires 5 args but whatever
    //     int damage = lambda(this, target, null, null, null);
    //     return damage;
    // }
    
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
    
    
}

