using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // TODO: plugin with Yarnspinner
    public static DialogueManager Instance { get; private set; }

    public void StartDialogue(string nodeName)
    {
        //GameManager.Instance.ChangeState(GameState.Dialogue);
        //UIManager.Instance.ShowDialogueUI();
        //dialogueRunner.StartDialogue(nodeName);
    }

    public void OnDialogueComplete()
    {
        //UIManager.Instance.HideDialogueUI();
        //GameManager.Instance.ChangeState(GameState.Overworld);
    }

/*
    [YarnCommand("playSFX")]
    public void PlaySFX(string id)
    {
        SoundManager.Instance.PlaySFX(id);
    }
    */

}
