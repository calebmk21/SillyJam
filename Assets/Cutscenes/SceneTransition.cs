// Script: https://discussions.unity.com/t/solved-how-to-change-scene-after-video-has-ended/760158/4

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneTransition : MonoBehaviour
{
    public VideoPlayer video;
    public string nextScene;

    void Awake()
    {
        video.loopPointReached += CheckOver;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(nextScene);
    }
}
