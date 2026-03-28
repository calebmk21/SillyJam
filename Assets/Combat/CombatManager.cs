using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public enum Phase
    {
        Start,
        Player,
        Enemy,
        End
    }

    private Phase _currentPhase = Phase.Start;
    
    
    void Awake()
    {
        Instance = this;
        _currentPhase = Phase.Start;
    }

    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadEnemyData()
    {
        
    }
    
}
