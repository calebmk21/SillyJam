using UnityEngine;
using System;
using System.Collections;

public class PlayerUnit : CombatUnit
{

    // floats for moving character sprite when it's their turn
    [SerializeField] private float _translateOffset = 2f;
    [SerializeField] private float _translationSpeed = 2f;
    
    private CharacterAnimatorController animationController;
    private CharacterAudioController audioController;
    
    //Events for UI
    public delegate void HealthEventHandler(int health);
    public event HealthEventHandler OnHealthChanged;

    //Events for Combat Manager
    public delegate void StartTurnEventHandler(PlayerUnit unit);
    public event StartTurnEventHandler OnStartTurn;
    public delegate void EndTurnEventHandler();
    public event EndTurnEventHandler OnEndTurn;
    
    
    // Base Attack Values
    private int baseDamageMultiplier = 1;
    private int rawDamage;
    
    // Targeting Events
    
    
    
    private delegate void DealDamageCallback(int damage);
    private DealDamageCallback _dealDamageCallback;
    
    protected virtual void Awake()
    {
        animationController = GetComponent<CharacterAnimatorController>();
        audioController = GetComponent<CharacterAudioController>();
    }

    protected override void Start()
    {
        base.Start();
        if (CombatManager.Instance != null)
        {
            //CombatManager.OnActiveTurnChanged += StartTurn(unit);
        }
    }

    protected virtual void Update()
    {

    }
    

    public virtual void Attack(EnemyUnit enemy)
    {
        // OnDisplayAlert("Attack");
        rawDamage = StandardDamageCalculator(baseDamageMultiplier);
        enemy.TakeDamage(rawDamage);
        //audioController.PlayAttackVoice();
        //animationController.PlayAttack();
        // OnTargetOther?.Invoke(this, enemy);
    }

    public virtual void UseAbility(Ability ability)
    {
        
    }
    
    public virtual void UseAbility(EnemyUnit enemy, Ability ability)
    {
        
    }
    
    
    protected override void StartTurn()
    {
        //audioController.PlayStartTurnVoice();
        StartCoroutine(MoveRight());
        OnStartTurn?.Invoke(this);
    }

    protected override void EndTurn()
    {
        DecrementStatusEffects();
        StartCoroutine(MoveLeft());
        OnEndTurn?.Invoke();
    }
    
    // public method for the Combat Manager to initialize this unit's turn from the queue
    public void ActivateUnitTurn()
    {
        StartTurn();
    }
    
    private IEnumerator MoveRight()
    {
        float startingXpos = transform.position.x;
        while (transform.position.x < startingXpos + _translateOffset)
        {
            transform.Translate(Vector3.right * _translationSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
    }
    
    private IEnumerator MoveLeft()
    {
        float startingXpos = transform.position.x;
        while (transform.position.x > startingXpos - _translateOffset)
        {
            transform.Translate(Vector3.right * _translationSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
    }
    
}
