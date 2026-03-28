using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Scriptable Objects/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    // Main details
    public string Name;
    public int Level;
    public GameObject character;

    // base values, before modification
    public int HP;
    public int MP;
    public int ATK;
    public int MAG;
    public int DEF;
    public int MDEF;
    public int SPD;

    
}
