using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Multiplayer.PlayMode;
using UnityEngine.Serialization;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }

    public enum BattlePhase
    {
        Start,
        Player,
        Enemy,
        Won,
        Lost
    }

    [Header("Combatants")]
    // collection of all units in the battle
    // initialize the encounter by sending a prefab of the encounter container into this instance when starting combat
    [SerializeField] private GameObject PartyContainer;
    [SerializeField] private GameObject EnemyContainer;
    public List<PlayerUnit> Party = new List<PlayerUnit>();
    public List<EnemyUnit> Enemies = new List<EnemyUnit>();


    [Header("Combat Data")] 
    [SerializeField] private CombatUI combatUI;
    private Queue<CombatUnit> TurnOrder = new Queue<CombatUnit>();
    private BattlePhase _currentPhase;
    public CombatUnit _currentUnit;
    public PlayerUnit _currentPlayer;
    private bool PartyAlive = true, EnemiesAlive = true;
    
    
    // Turn Logic
    public delegate void ActiveTurnEvent(bool value);
    public static ActiveTurnEvent OnActiveTurnChanged;
    private bool _isActiveTurn = false;

    public delegate void NewTurn(CombatUnit unit);
    public static NewTurn OnNewTurn;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void OnEnable()
    {
        CombatUI.OnSelectEnemyAttack += ChoseAttack;
        CombatUI.OnSelectEnemyAbility += ChoseAbility;
    }

    public void Start()
    {
        LoadCombatants();
        GetTurnOrder();
        StartCoroutine(CombatRound());
    }
    
    
    #region Combat

    // Main Combat Coroutine -- call after every turn
    // TODO add a StartCoroutine(Combat()); once a combat button is selected in PlayerPhase
    private IEnumerator CombatRound()
    {
        if (_currentPhase != BattlePhase.Won && _currentPhase != BattlePhase.Lost)
        {
            if (TurnOrder.Count == 0)
            {
                GetTurnOrder();
            }

            _currentUnit = TurnOrder.Dequeue();

            Debug.Log(_currentUnit.GetName());

            if (_currentUnit.GetType() == typeof(PlayerUnit))
            {
                _currentPhase = BattlePhase.Player;
                _currentPlayer = _currentUnit as PlayerUnit;
                yield return StartCoroutine(PlayerPhase(_currentPlayer));
            }
            else if (_currentUnit.GetType() == typeof(EnemyUnit))
            {
                _currentPhase = BattlePhase.Enemy;
                EnemyUnit enemyUnit = _currentUnit as EnemyUnit;
                yield return StartCoroutine(EnemyPhase(enemyUnit));
            }

        }

        if (_currentPhase == BattlePhase.Won || _currentPhase == BattlePhase.Lost)
        {
            yield return StartCoroutine(BattleEnd(_currentPhase));
        }
    }

    // Player turn logic
    private IEnumerator PlayerPhase(PlayerUnit current)
    {
        
        // Give time for the wonky teleport
        yield return new WaitForSeconds(1);
        // display battle ui and give control to player
        current.ActivateUnitTurn();
        
    }
    
    // Enemy turn logic
    private IEnumerator EnemyPhase(EnemyUnit current)
    {
        // Maybe highlight the enemy?
        yield return new WaitForSeconds(1);
    
        //EnemyTurn();
    }
    
    
    
    
    #endregion


    #region Loading and Data
    
    private void StartTurn(PlayerUnit unit)
    {
        _currentUnit = unit;
        _isActiveTurn = true;
        // OnActiveTurnChanged.Invoke(_isActiveTurn);
        //BattleUIHandler.Instance.ToggleActionMenu(true);
    }

    private void StartTurn(EnemyUnit enemy)
    {
        _isActiveTurn = true;
        OnActiveTurnChanged.Invoke(_isActiveTurn);
    }
    
    private void EndTurn()
    {
        _isActiveTurn = false;
        //Micro delay to allow for any remaining animations to finish.
        StartCoroutine(CombatRound());
    }
    
    public void LoadCombatants()
    {
        // gathering all combat data
        foreach (Transform child in EnemyContainer.transform)
        {
            EnemyUnit enemy = child.gameObject.GetComponent<EnemyUnit>();
            Enemies.Add(enemy);
            enemy.OnStartTurn += StartTurn;
            enemy.OnEndTurn += EndTurn;
        }
        
        foreach (Transform child in PartyContainer.transform)
        {
            PlayerUnit player = child.gameObject.GetComponent<PlayerUnit>();
            Party.Add(player);
            player.OnStartTurn += StartTurn;
            player.OnEndTurn += EndTurn;
        }
        
    }
    
    public void ChoseAttack(EnemyUnit enemy)
    {
        _currentPlayer.Attack(enemy);
    }

    public void ChoseAbility(Ability ability)
    {
        _currentPlayer.UseAbility(ability);
    }

    public void ChoseAbility(EnemyUnit enemy, Ability ability)
    {
        _currentPlayer.UseAbility(enemy, ability);
    }

    public CombatUnit GetCurrentCombatant()
    {
        return _currentUnit;
    }
    
    public void GetTurnOrder()
    {
        Dictionary<CombatUnit, float> unitSpeeds = new Dictionary<CombatUnit, float>();
        foreach (CombatUnit unit in Enemies)
        {
            unitSpeeds.Add(unit, unit.GetSpeed());
        }
        foreach (CombatUnit unit in Party)
        {
            unitSpeeds.Add(unit, unit.GetSpeed());
        }
        
        List<float> speedValues = new List<float>();
        
        foreach (KeyValuePair<CombatUnit, float> kvp in unitSpeeds)
        {
            speedValues.Add(kvp.Value);
        }
        
        speedValues.Sort();
        speedValues.Reverse();
        
        foreach (int speed in speedValues)
        {
            CombatUnit _curr; 
            
            _curr = unitSpeeds.FirstOrDefault(x => x.Value == speed).Key;
            TurnOrder.Enqueue(_curr);
            //Debug.Log("Adding " + _curr.GetName() + " to turn order with " + speed);
            unitSpeeds.Remove(_curr);
        }

    }
    #endregion
    #region UI Controls
    #endregion
    #region Battle Transitions

    private IEnumerator BattleEnd(BattlePhase phase)
    {
        yield return new WaitForSeconds(1);
    }

    #endregion

    // prepare to hand control back over to the game manager
    public void ResetCombatManager()
    {
        EnemyContainer = null;
        Enemies.Clear();
        TurnOrder.Clear();
        _currentUnit = null;
    }
    
    
}
