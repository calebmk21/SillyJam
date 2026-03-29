using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Enum to represent different GameStates
public enum GameState
{
    MainMenu,
    Overworld,
    Dialogue,
    Combat,
    GameOver,
    GameWin
}

public class GameManager : MonoBehaviour
{
    /**
     * The GameManager follows the singleton pattern. It is essentially
     * a state machine (or an orchestrator of the game). 
     *
     * It handles:
     *   - Scene Switching
     *   - Initiation of combat, dialogue
     *   - Sound
     *
     * Each Manager that it calls should call ChangeState to change to the appropriate
     * state. That way, it returns control to the GameManager without the GameManager
     * having to listen for events. e.g. The GameManger will initiate dialogue with
     * the DialogueManager, and the DialogueManager will call back to the GameManager
     * to go to the appropriate state based on changes in the DialogueManager.
    **/
    public static GameManager Instance { get; private set; }
    public GameState currentState { get; private set; }
    public UIManager currentUIManager { get; private set; }

    [SerializeField] private SceneRegistry _sceneRegistry;
    [SerializeField] private GameState _startingGameState; // serialized to allow dev override. By default, value is MainMenu

    private Dictionary<SceneID, string> _scenes;
    private bool _isPaused;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _scenes = new Dictionary<SceneID, string>();
        foreach (var s in _sceneRegistry.scenes)
        {
            if (!_scenes.ContainsKey(s.id))
                _scenes.Add(s.id, s.sceneName);
        }

        _isPaused = false;
    }

    void Start()
    {
        ChangeState(_startingGameState);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                Debug.Log("MainMenu");
                LoadScene(SceneID.MainMenu);
                SoundManager.Instance.PlayMusic(AudioID.MainMenu);
                break;

            case GameState.Overworld:
                Debug.Log("Overworld");
                LoadScene(SceneID.Overworld);
                SoundManager.Instance.PlayMusic(AudioID.Overworld);
                break;

            case GameState.Dialogue:
                //DialogueManager.Instance.StartDialogue();
                Debug.Log("Dialogue");
                SoundManager.Instance.PlayMusic(AudioID.Dialogue);
                break;

            case GameState.Combat:
                Debug.Log("Combat");
                //CombatManager.Instance.StartCombat();
                SoundManager.Instance.PlayMusic(AudioID.Dialogue);
                break;

            case GameState.GameOver:
                Debug.Log("GameOver");
                LoadScene(SceneID.GameOver);
                SoundManager.Instance.PlayMusic(AudioID.GameOver);
                break;

            case GameState.GameWin:
                Debug.Log("GameWin");
                LoadScene(SceneID.GameWin);
                SoundManager.Instance.PlayMusic(AudioID.GameWin);
                break;
        }
    }

    public void LoadScene(SceneID sceneId)
    {
        if (!_scenes.TryGetValue(sceneId, out string sceneName))
        {
            Debug.LogError($"Scene ID '{sceneId}' not found.");
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        currentUIManager = FindAnyObjectByType<UIManager>();

        if (currentUIManager == null)
        {
            Debug.LogWarning("No UIManager found in this scene.");
        }
    }

    public void StartGame()
    {
        ChangeState(GameState.Overworld);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentUIManager.ShowPanel(PanelID.PauseMenu);
        _isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        currentUIManager.HideAllPanels();
        _isPaused = false; 
    }

    public bool IsPaused()
    {
        return _isPaused;
    }
}