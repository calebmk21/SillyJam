using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public enum Phase
    {
        Start,
        Player,
        Transition,
        Enemy,
        End
    }

    private Phase _currentPhase = Phase.Start;

    [Header("Combat Data")]
    [SerializeField] private Queue<GameObject> TurnQueue; 
    
    
    void Awake()
    {
        Instance = this;
        _currentPhase = Phase.Start;
        LoadCombatants();

    }

    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Loading and Data
    public void LoadCombatants()
    {
        
    }

    public void LoadPlayerData()
    {
        
    }

    public void LoadEnemyData()
    {
        
    }

    public void GetTurnOrder()
    {
        
    }
    #endregion



    #region UI Controls
    #endregion

    #region Battle Transitions

    

    #endregion
    
}
