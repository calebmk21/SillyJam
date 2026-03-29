using System.Collections.Generic;
using UnityEngine;

public enum AudioID
{
    // Music
    MainMenu,
    Overworld,
    Combat,
    Dialogue,
    GameOver,
    GameWin,
    // SFX
    ButtonClick
}

[System.Serializable]
public class AudioEntry
{
    public AudioID id;
    public AudioClip clip;
}


[CreateAssetMenu(fileName = "AudioRegistry", menuName = "Scriptable Objects/AudioRegistry")]
public class AudioRegistry : ScriptableObject
{
    public List<AudioEntry> audioClips;
}