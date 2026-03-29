using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    /**
     * The SoundManager is a singleton that controls all sound in the game.
     * That is, it plays all master music and SFX. It also controls volume.
    **/
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioRegistry _audioRegistry;

    private Dictionary<AudioID, AudioClip> _audioClips;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _audioClips = new Dictionary<AudioID, AudioClip>();
        foreach (var entry in _audioRegistry.audioClips)
            _audioClips[entry.id] = entry.clip;
    }

    public void PlaySFX(AudioID audioId)
    {
        if (_audioClips.TryGetValue(audioId, out AudioClip sfxClip))
        {
            _sfxSource.PlayOneShot(sfxClip);
        }
    }
    public void PlayMusic(AudioID audioId)
    {
        if (_audioClips.TryGetValue(audioId, out AudioClip musicClip) && _musicSource.clip != musicClip)
        {
            _musicSource.clip = musicClip;
            _musicSource.Play();
        }
    }

    public void SetMasterVolume(float volume)
    {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    public float GetMasterVolumeLinear()
    {
        _audioMixer.GetFloat("MasterVolume", out float volume);
        return Mathf.Pow(10f, volume / 20f);
    }
    public float GetMusicVolumeLinear()
    {
        _audioMixer.GetFloat("MusicVolume", out float volume);
        return Mathf.Pow(10f, volume / 20f);
    }
    public float GetSFXVolumeLinear()
    {
        _audioMixer.GetFloat("SFXVolume", out float volume);
        return Mathf.Pow(10f, volume / 20f);
    }
}