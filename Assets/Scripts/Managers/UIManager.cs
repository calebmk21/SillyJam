using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PanelID
{
    PauseMenu,
    SettingsMenu,
    Credits
}

public class UIManager : MonoBehaviour
{
    /**
     * The UIManager is responsible for all things UI.
     * This includes showing panels, button triggers, and more.
    **/
    public static UIManager Instance { get; private set; }

    // Panels
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _credits;
    private Dictionary<PanelID, GameObject> _panels;

    // Sliders
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        _panels = new Dictionary<PanelID, GameObject>();

        if (_pauseMenu != null)
        {
            _panels.Add(PanelID.PauseMenu, _pauseMenu);
        }
        if (_settingsMenu != null)
        {
            _panels.Add(PanelID.SettingsMenu, _settingsMenu);
        }
        if (_credits != null)
        {
            _panels.Add(PanelID.Credits, _credits);
        }
    }

    void Start()
    {
        if (_masterSlider != null)
        {
            _masterSlider.value = SoundManager.Instance.GetMasterVolumeLinear();
        }

        if (_musicSlider != null)
        {
            _musicSlider.value = SoundManager.Instance.GetMusicVolumeLinear();
        }

        if (_sfxSlider != null)
        {
            _sfxSlider.value = SoundManager.Instance.GetSFXVolumeLinear();
        }
    }

    public void HideAllPanels()
    {
        foreach (var panel in _panels.Values)
        {
            panel.SetActive(false);
        }

    }

    public void ShowPanel(PanelID panelId)
    {
        HideAllPanels();
        if (_panels.TryGetValue(panelId, out GameObject panel))
        {
            panel.SetActive(true);
        }
    }

    // Main Menu
    public void OnStartGamePressed()
    {
        PlayButtonSound();
        GameManager.Instance.StartGame();
    }

    public void OnCreditsPressed()
    {
        PlayButtonSound();
        ShowPanel(PanelID.Credits);
    }

    public void OnQuitPressed()
    {
        PlayButtonSound();
        Application.Quit();
    }

    // Pause / Resume
    public void TogglePauseButton()
    {
        PlayButtonSound();
        if (GameManager.Instance.IsPaused())
        {
            GameManager.Instance.ResumeGame();
        }
        else
        {
            GameManager.Instance.PauseGame();
        }
    }

    // Settings
    public void OnSettingsButtonPressed()
    {
        PlayButtonSound();
        ShowPanel(PanelID.SettingsMenu);
    }

    public void OnBackButtonPressed()
    {
        PlayButtonSound();
        HideAllPanels();
    }

    // Game Over / Game Win Buttons
    public void OnReturnToMenuPressed()
    {
        PlayButtonSound();
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }

    // Volume
    public void OnMasterVolumeChanged(float volume)
    {
        SoundManager.Instance.SetMasterVolume(volume);
    }

    public void OnMusicVolumeChanged(float volume)
    {
        SoundManager.Instance.SetMusicVolume(volume);
    }

    public void OnSFXVolumeChanged(float volume)
    {
        SoundManager.Instance.SetSFXVolume(volume);
    }

    // Helper Functions
    public void PlayButtonSound()
    {
        SoundManager.Instance.PlaySFX(AudioID.ButtonClick);
    }
}
