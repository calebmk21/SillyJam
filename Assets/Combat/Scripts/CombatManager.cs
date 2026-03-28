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
    private PlayerCombat _currentPartyMember;


    [Header("Combat Data")]
    [SerializeField] private Queue<GameObject> TurnQueue; 
    [SerializeField] private GameObject partyPrefab;
    
    
    
    void Awake()
    {
        Instance = this;
        _currentPhase = Phase.Start;
        //LoadCombatants();

    }

    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Combat Phases

    private IEnumerator InitializeCombat()
    {
        // GameObject party = Instantiate(partyPrefab);
        // PlayerCombat MainChar = party.transform.Find("MainChar").GetComponent<PlayerCombat>();
        


        yield return null;
    }

    private IEnumerator PlayerPhase;
    private IEnumerator EnemyPhase;
    #endregion


    #region Loading and Data
    public void LoadCombatants(GameObject enemies)
    {
        GameObject party = Instantiate(partyPrefab);



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

    public void NextPhase()
    {
        
    }

    public void EndBattle()
    {
        
    }

    

    #endregion
    
}
