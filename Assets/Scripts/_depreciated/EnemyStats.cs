using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    // identification
    public int ID;
    public string Name;
    public int EXP;
    
    // stats
    public int HP;
    public int MP;
    public int ATK;
    public int MAG;
    public int DEF;
    public int MDEF;
    public int SPD;

}
