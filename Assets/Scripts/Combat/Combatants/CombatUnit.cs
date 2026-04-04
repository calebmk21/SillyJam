using System;
using JetBrains.Annotations;
using NUnit.Framework.Interfaces;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Random = UnityEngine.Random;

public abstract class CombatUnit : MonoBehaviour
{
    [Header("Data Containers")]
    [SerializeField] protected string charName = "";
    [SerializeField] protected int currentHealth = 100;
    [SerializeField] protected List<AbilityData> abilitiesData;
    protected readonly List<Ability> abilities;
    public readonly ReadOnlyCollection<Ability> Abilities;
    
    [Header("Stat Info")] 
    [SerializeField] protected CharacterStat HP;
    [SerializeField] protected CharacterStat ATK;
    [SerializeField] protected CharacterStat MAG;
    [SerializeField] protected CharacterStat DEF;
    [SerializeField] protected CharacterStat MDEF;
    [SerializeField] protected CharacterStat SPD;
    public readonly List<CharacterStat> Stats;

    public string Name { get { return charName; } }
    public int CurrentHealth { get { return currentHealth; } }
    public int MaxHealth { get { return HP.Value; } }

    // [Header("Equipment Info")] 
    // [SerializeField]  Equipment Armor;
    // [SerializeField]  Equipment Weapon;
    // [SerializeField]  Equipment Accessory;
    
    [Header("Status Info")]
    protected Dictionary<StatusEffectData, StatusEffect> statusEffects;
    protected bool isAlive = true;
    protected bool isActiveTurn = false;

    // Methods for turn logic
    protected abstract void StartTurn();
    protected abstract void EndTurn();

    public delegate void SetTurnActive();
    public SetTurnActive SetTurn;
    
    
    public CombatUnit()
    {
        abilitiesData = new List<AbilityData>();
        abilities = new List<Ability>();
        statusEffects = new Dictionary<StatusEffectData, StatusEffect>();
        Abilities = abilities.AsReadOnly();
        
        // Stats
        
        HP = new CharacterStat(StatType.MaxHealth);
        ATK = new CharacterStat(StatType.Attack);
        MAG = new CharacterStat(StatType.Magic);
        DEF = new CharacterStat(StatType.Defense);
        MDEF = new CharacterStat(StatType.MagicDefense);
        SPD = new CharacterStat(StatType.Speed);

        Stats = new List<CharacterStat>()
        {
            HP,
            ATK,
            MAG,
            DEF,
            MDEF,
            SPD
        };
    }
    
    
    protected virtual void Start()
    {
        currentHealth = MaxHealth;
    }

    protected void InitializeAbilities()
    {
        foreach (AbilityData data in abilitiesData)
        {
            Ability ab = data.Initialize(this.gameObject);
            abilities.Add(ab);
        }
    }

    // public virtual void UseAbility(Ability ability, CombatUnit target)
    // {
    //     switch (ability.GetType().ToString())
    //     {
    //         case "AttackAbility":
    //             AttackAbility attackAbility = ability as AttackAbility;
    //             attackAbility.Trigger(this, target, out float rawDamage);
    //             break;
    //         case "SupportAbility":
    //             break;
    //         default:
    //             break;
    //     }
    // }

    public virtual int StandardDamageCalculator(int mult, int critMultiplier = 2)
    {
        float damageVariance = Random.Range(0.90f, 1.10f);
        float effectiveAttack = mult * damageVariance * ATK.Value;
        
        // currently no crit stat, so it's based on speed because why not
        float critChance = Random.value;
        float speed = (float)SPD.Value;
        float critThreshold = speed * 0.25f / (10f + speed);

        // Crits deal double damage
        if (critThreshold > critChance)
        {
            effectiveAttack *= critMultiplier;
        }

        int rounded = (int)Math.Round(effectiveAttack, 0);
        return rounded; 
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth = math.max(0, currentHealth - damage);
    }

    public virtual void RestoreHealth(CombatUnit target, int healing)
    {
        currentHealth = math.min(MaxHealth, currentHealth + healing);
    }

    #region Getters/Setters
    public string GetName()
    {
        return name;
    }

    // Stats
    public int GetAttack()
    {
        return ATK.Value;
    }

    public int GetDefense()
    {
        return DEF.Value;
    }

    public int GetMagic()
    {
        return MAG.Value;
    }

    public int GetMagicDefense()
    {
        return MDEF.Value;
    }
        public int GetSpeed()
    {
        return SPD.Value;
    }



    #endregion
    #region Stat Modifications
    

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

    public void AddStatMod(StatMod mod)
    {
        foreach (CharacterStat stat in Stats)
        {
            if (stat.type == mod.StatType)
            {
                stat.AddModifier(mod);
            }
        }
    }

    public void RemoveStatMod(StatMod mod)
    {
        foreach (CharacterStat stat in Stats)
        {
            if (stat.type == mod.StatType)
            {
                stat.RemoveModifier(mod);
            }
        }
    }

    public void RemoveAllStatModsFromSource(GameObject source)
    {
        foreach (CharacterStat stat in Stats)
        {
            stat.RemoveAllModifiers(source);
        }
    }
    
    protected void DecrementStatusEffects()
    {
        foreach (StatusEffect status in statusEffects.Values.ToList()) //creates copy into a list to iterate with. Avoids error if iterating and operating in oringal dict.
        {
            status.DecrementTurnDuration();
            if (status.isFinished)
            {
                statusEffects.Remove(status.Data);
            }
        }
    }

    #endregion

    // public float DamageCalculator(Func<CombatUnit, CombatUnit, Null, Null, Null, int> lambda, CombatUnit target)
    // {
    //     // not sure why it requires 5 args but whatever
    //     int damage = lambda(this, target, null, null, null);
    //     return damage;
    // }
    
    #region Debug
    public void DisplayStatInfo()
    {

    }
    #endregion
    
    
}

