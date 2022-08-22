using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public string messageForLoading = "";
    public bool isTransitioning = false;

    AudioSource audioSource;
    Movement movement;
    [SerializeField] GameObject ui;
    UI userInterface;

    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    private void Start()
    {
        userInterface = ui.GetComponent<UI>();
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                DisableMovementAndPlayAudio(successSound);
                messageForLoading = "You have reached the landing pad!\n" +
                "Waiting to refuel the rocket for the next destination!";
                userInterface.PrintTextForLoading(messageForLoading);
                PlayParticleEffects(successParticles);
                StartCoroutine(WaitSecondsBeforeLoadingScene(4f, true));
                break;
            default:
                DisableMovementAndPlayAudio(crashSound);
                messageForLoading = "You crashed!\n" +
                "Waiting to fix the rocket, you will start from the launch pad!";
                userInterface.PrintTextForLoading(messageForLoading);
                PlayParticleEffects(crashParticles);
                StartCoroutine(WaitSecondsBeforeLoadingScene(4f, false));
                break;
        }
    }

    private void DisableMovementAndPlayAudio(AudioClip sound)
    {
        isTransitioning = true;
        audioSource.Stop();
        movement.enabled = false;
        movement.StopThrusting();
        movement.StopRotating();
        audioSource.PlayOneShot(sound);
    }

    public void PlayParticleEffects(ParticleSystem particle)
    {
        particle.Play();
    }

    public void StopParticleEffects(ParticleSystem particle)
    {
        particle.Stop();
    }

    void ReloadCurrentLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        print("Reloaded current level");
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //Se ho raggiunto la fine dei livelli ricarico il primo
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        print("Loaded next level");
    }

    //se bool è true lo script carica il prossimo livello, altrimenti ricarica il livello attuale
    IEnumerator WaitSecondsBeforeLoadingScene(float delayTime, bool choice)
    {
        yield return new WaitForSeconds(delayTime);
        if(choice)
        {
            LoadNextLevel();
        }
        else
        {
            ReloadCurrentLevel();
        }
    }
}
