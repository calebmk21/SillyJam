using System.Collections.Generic;
using UnityEngine;

public enum SceneID
{
    MainMenu,
    Overworld,
    GameOver,
    GameWin
}

[System.Serializable]
public struct SceneReference
{
    public SceneID id;
    public string sceneName;
}

[CreateAssetMenu(fileName = "SceneRegistry", menuName = "Scriptable Objects/SceneRegistry")]
public class SceneRegistry : ScriptableObject
{
    public List<SceneReference> scenes;
}

