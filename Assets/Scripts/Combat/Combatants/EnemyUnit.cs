using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : CombatUnit
{
    protected List<PlayerUnit> party;

    public delegate void StartTurnEventHandler(EnemyUnit enemy);
    public event StartTurnEventHandler OnStartTurn;
    public delegate void EndTurnEventHandler();
    public event EndTurnEventHandler OnEndTurn;

    protected override void Start()
    {
        base.Start();
        party = CombatManager.Instance.Party;
    }
    
    protected virtual void Attack(PlayerUnit unit)
    {
        Debug.Log(gameObject.name + " attacked " + unit.gameObject.name);
        //OnDisplayAlert("Attack");
        // unit.TakeDamage(CalculateDamage(baseDamageMultiplier));
        StartCoroutine(DelayEndTurn(1));
        //EndTurn();
    }

    protected virtual PlayerUnit PickRandomHero()
    {
        int index = Random.Range(0, party.Count);
        PlayerUnit unit = party[index];
        return unit;
    }

    protected override void StartTurn()
    {
        OnStartTurn?.Invoke(this);
        Attack(PickRandomHero());
    }

    protected override void EndTurn()
    {
        OnEndTurn?.Invoke();
    }

    protected void OnDestroy()
    {
        // if (FindObjectOfType<CombatManager>())
        // {
        //     CombatManager.OnActiveTurnChanged -= ToggleTurnTimer;
        // }
    }

    public IEnumerator DelayEndTurn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        EndTurn();
    }
}