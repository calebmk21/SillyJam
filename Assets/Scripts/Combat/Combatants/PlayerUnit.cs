using UnityEngine;

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
    
    
    private delegate void DealDamageCallback(float damage);
    private DealDamageCallback _dealDamageCallback;
    
    protected virtual void Awake()
    {
        animationController = GetComponent<CharacterAnimatorController>();
        audioController = GetComponent<CharacterAudioController>();
    }


    public virtual void Attack(EnemyUnit enemy)
    {
        
    }
    
    protected override void StartTurn()
    {
        audioController.PlayStartTurnVoice();
        //StartCoroutine(MoveLeft());
        OnStartTurn?.Invoke(this);
    }

    protected override void EndTurn()
    {
        DecrementStatusEffects();
        //StartCoroutine(MoveRight());
        OnEndTurn?.Invoke();
    }    
    
}
