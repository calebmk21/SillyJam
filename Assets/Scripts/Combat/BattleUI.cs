using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [Header("Character Data")] 
    [SerializeField] private CombatUnit character;
    private BattleActions[] battleActions = new BattleActions[4];
    
    public void LoadCharacterActions(CombatUnit newPlayer)
    {
        ChangePlayer(newPlayer);

    }
    
    
    public void ChangePlayer(CombatUnit newPlayer)
    {
        character = newPlayer;
    }


    
}
