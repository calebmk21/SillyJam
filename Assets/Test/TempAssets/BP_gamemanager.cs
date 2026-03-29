// using System;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.Rendering;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;
//
// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance;
//
//     // public int dayNumber = 0;
//     // public int finalDay = 7;
//     public float maxTimeToFreeze = 1000f;
//     public float currentTime;
//     public Journal journal;
//     
//     public Ending route;
//     public bool diedToSeal = false, diedOfHypothermia = false;//, diedToWater = false;
//     public bool died = false, nearWarmth = false;
//
//     public int warmItems = 0, maxWarmItems = 5;
//
//     public GameObject tutorialPanel;
//
//     public bool calvinFuckingLosesIt;
//
//     public bool isWarm;
//
//     public Slider freezeMeter;
//
//     public AITarget AITarget;
//     
//     public AudioSource bgm_exploration, bgm_campsite, bgm_chase;
//
//     public AudioSource sfx;
//
//     float defaultVolume = 0.4f;
//     float transitionTime = 0.5f;
//
//     public AudioSource sealAudio;
//     
//     public AudioClip mainMusic;
//     public AudioClip seal;
//     public AudioClip sealButFromBrooklyn;
//     public AudioClip campsite;
//
//     public JournalUI journalUI;
//
//     public bool isSealChasing;
//
//     public AudioClip[] Seal_Slurs;
//
//     //public AudioClip pageturn;
//
//     public Sprite itemInRange;
//     public Sprite inTheCold;
//     public Sprite ouch;
//     public Sprite canAnalyze;
//     public Sprite leavingTime;
//     //public Sprite currentSprite;
//     public Sprite emptyIndicator;
//     
//     public Image indicationMarker;
//     
//     void Awake()
//     {
//         Instance = this;
//         route = Ending.Neutral;
//
//         //currentSprite = null;
//         
//         Cursor.lockState = CursorLockMode.None;
//         Cursor.visible = true;
//         
//         OpeningSequence();
//     }
//     
//     void Start()
//     {
//         currentTime = maxTimeToFreeze;
//         freezeMeter.value = maxTimeToFreeze;
//         //bgm.clip = mainMusic;
//         //bgm.loop = true;
//         
//         // can comment this out if journal is already active in hierarchy (preferable) 
//         journalUI.gameObject.SetActive(true);
//         Cursor.lockState = CursorLockMode.None;
//         Cursor.visible = true;
//     }
//
//     void Update()
//     {
//         if (freezeMeter.value == 0f)
//         {
//             died = true;
//             diedOfHypothermia = true;
//             EndingSequence();
//         }
//         else if (!nearWarmth)
//         {
//             // meter depletes slower the more warm items you have
//             freezeMeter.value -= 4 * (maxWarmItems - warmItems) * Time.deltaTime;
//         }
//         // being near warmth brings your warmth meter back up
//         else if (nearWarmth && !died)
//         {
//            
//             if (freezeMeter.value < maxTimeToFreeze)
//             {
//                 freezeMeter.value += 80 * (1 + warmItems) * Time.deltaTime;
//             }
//             else
//             {
//                 freezeMeter.value = maxTimeToFreeze;
//             }
//         }
//     }
//
//     public void OpeningSequence()
//     {
//         // Tutorial sequence
//         Time.timeScale = 0f;
//         tutorialPanel.SetActive(true);
//         
//         Cursor.lockState = CursorLockMode.None;
//         Cursor.visible = true;
//     }
//     
//     
//     
//     
//     public enum Ending
//     {
//         Neutral,
//         Conference,
//         Detective,
//         Dead,
//         Coward
//     }
//
//     public void UpdateEnding(Ending newEnding)
//     {
//         switch (newEnding)
//         {
//             case (Ending.Neutral):
//                 route = Ending.Neutral;
//                 break;
//             case (Ending.Conference):
//                 route = Ending.Conference;
//                 break;
//             case (Ending.Detective):
//                 route = Ending.Detective;
//                 break;
//             case (Ending.Dead):
//                 route = Ending.Dead;
//                 break;
//             case (Ending.Coward):
//                 route = Ending.Coward;
//                 break;
//         }
//     }
//
//     public void EndingSequence()
//     {
//         
//         // maybe add a fade to black? 
//         
//         // uses the journal to calculate the ending obtained
//         journal.EvaluateEnding();
//         
//         // transition to ending scenes
//         string endingString = "FailsafeEnding";
//
//         switch (route)
//         {
//             case (Ending.Neutral):
//                 endingString = "NeutralEnding";
//                 break;
//             case (Ending.Conference):
//                 endingString = "ConferenceEnding";
//                 break;
//             case (Ending.Detective):
//                 endingString = "DetectiveEnding";
//                 break;
//             case (Ending.Dead):
//                 if (diedToSeal)
//                 {
//                     endingString = "SealEnding";
//                 }
//                 else if (diedOfHypothermia)
//                 {
//                     endingString = "FrozenEnding";
//                 }
//                 else
//                 {
//                     endingString = "FailsafeEnding";
//                 }
//                 break;
//             case (Ending.Coward):
//                 endingString = "CowardEnding";
//                 break;
//         }
//
//         
//         // Debug.Log("You died of hypothermia");
//         SceneManager.LoadScene(endingString);
//
//     }
//     
//     //This is the place where I put the AudioManager stuff in because I'm stupid - Calvin
//     public void ChangeMusic()
//     {
//         if (isSealChasing == true)
//             return;
//         
//         print("this is triggering!");
//         AudioSource nowPlaying = bgm_exploration;
//         AudioSource target = bgm_campsite;
//         if (nowPlaying.isPlaying == false)
//         {
//             nowPlaying = bgm_campsite;
//             target = bgm_exploration;
//         }
//
//         StartCoroutine(MixSources(nowPlaying, target));
//         
//     }
//     public void SealAttackMusic()
//     {
//         print("OH FUCK HE'S GONNA KILL ME!");
//         AudioSource nowPlaying = bgm_exploration;
//         AudioSource target = bgm_chase;
//
//         if (nowPlaying.isPlaying == false)
//         {
//             nowPlaying = bgm_campsite;
//             target = bgm_chase;
//         }
//
//         nowPlaying.Pause();
//         target.Play();
//         nowPlaying.volume = 0f;
//         target.volume = 0.1f;
//     }
//
//     public void SealAttackFadeOut()
//     {
//         print("HE'S GOING AWAY!");
//         AudioSource nowPlaying = bgm_chase;
//         AudioSource target;
//
//         if (isWarm == true)
//         {
//             target = bgm_campsite;
//         }
//         else
//         {
//             target = bgm_exploration;
//         }
//
//         StartCoroutine(SealWinddown(nowPlaying, target));
//     }
//
//     public IEnumerator SealWinddown(AudioSource nowPlaying, AudioSource target)
//     {
//
//         float percentage = 0;
//             while (nowPlaying.volume > 0)
//             {
//                 nowPlaying.volume = Mathf.Lerp(0.1f, 0, percentage);
//                 percentage += Time.deltaTime / 1.25f;
//                 yield return null;
//             }
//
//         nowPlaying.Stop();
//         if (target.isPlaying == false)
//             target.Play();
//         target.UnPause();
//         percentage = 0;
//
//         while (target.volume < 0.4f)
//         {
//             target.volume = Mathf.Lerp(0, 0.4f, percentage);
//             percentage += Time.deltaTime / 1.25f;
//             yield return null;
//         }
//     }
//     IEnumerator MixSources(AudioSource nowPlaying, AudioSource target)
//     {
//         float percentage = 0;
//         while (nowPlaying.volume > 0)
//         {
//             nowPlaying.volume = Mathf.Lerp(defaultVolume, 0, percentage);
//             percentage += Time.deltaTime / transitionTime;
//             yield return null;
//         }
//
//         nowPlaying.Pause();
//         if (target.isPlaying == false)
//             target.Play();
//         target.UnPause();
//         percentage = 0;
//
//         while (target.volume < defaultVolume)
//         {
//             target.volume = Mathf.Lerp(0, defaultVolume, percentage);
//             percentage += Time.deltaTime / transitionTime;
//             yield return null;
//         }
//     }
//     
//     public void InvokeBrooklyn()
//     {
//         InvokeRepeating("BrooklynSeal", 0f, 3f);
//     }
//
//     public void BrooklynSeal()
//     {
//         int randomIndex = UnityEngine.Random.Range(0, Seal_Slurs.Length);
//         sealAudio.clip = Seal_Slurs[randomIndex];
//         sealAudio.Play();
//         print("That seal needs to watch his mouth.");
//     }
//     public void StopInvokeBrooklyn()
//     {
//         CancelInvoke();
//     }
// }