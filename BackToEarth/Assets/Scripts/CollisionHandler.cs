using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public string messageForLoading = "";
    public bool showMessage = false;
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                Debug.Log("Congrats, you finished!");
                messageForLoading = "You have reached the landing pad!\n" +
                "Waiting to refuel the rocket for the next destination!";
                StartCoroutine(WaitSecondsBeforeLoadingScene(3f, true));
                break;
            default:
                Debug.Log("Sorry, you blew up!");
                messageForLoading = "You crashed!\n" +
                "Waiting to fix the rocket, you will start from the last launch pad you reached!";
                StartCoroutine(WaitSecondsBeforeLoadingScene(3f, false));
                break;
        }
    }

    void ReloadCurrentLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        print("Reloaded current level");
    }

    void LoadNextLevel()
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
        showMessage = true;
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
