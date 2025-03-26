using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ParticleSystemJobs;
using System;
using UnityEngine.InputSystem;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public AudioClip audioClip2;
    [SerializeField] ParticleSystem finishEffect;
    private AudioSource audioSource;
    bool isControl = true;
    float levelLoadDelay = 2f;

     void Update()
    {
        ResponseToDebugKey();
    }

     void ResponseToDebugKey()
    {
        if (Keyboard.current.lKey.IsPressed())
        {
            LoadNextLevel();
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();


    }
    private void OnCollisionEnter(Collision other)
    {
        if (!isControl)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "StartPlatform":
                Debug.Log("Start");
                break;
            case "FinishPlatform":
                GetComponent<Movements>().enabled = false;
                Invoke("LoadNextLevel", levelLoadDelay);
                finishParticleEffect();
                break;
            default:
                audioSource.PlayOneShot(audioClip2);
                StartCrashSequens();
                break;
        }

    }
    void finishParticleEffect()
    {
        finishEffect.Play();

    }
    void StartCrashSequens()
    {
        isControl = false;
        GetComponent<Movements>().enabled = false;
        Invoke("ReloadCurrentLevel", levelLoadDelay);



    }
    void ReloadCurrentLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);

    }
    void LoadNextLevel()
    {

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentScene + 1;
        SceneManager.LoadScene(nextSceneIndex);

    }

}
