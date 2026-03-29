using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [Header("Character Data")] 
    [SerializeField] private CombatUnit character;
    [SerializeField] private Button action1, action2, action3, action4;
    private TMPro.TextMeshProUGUI text1, text2, text3, text4;
    private BattleActions[] battleActions = new BattleActions[4];
    
    public void LoadCharacterActions(CombatUnit newPlayer)
    {
        ChangePlayer(newPlayer);
        battleActions = character.GetBattleActions();
    }
    
    
    public void ChangePlayer(CombatUnit newPlayer)
    {
        character = newPlayer;
    }


    
}
