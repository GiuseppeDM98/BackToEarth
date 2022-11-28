using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI : MonoBehaviour
{
    Scene scene;
    [SerializeField] TextMeshProUGUI textForLoading;
    [SerializeField] TextMeshProUGUI textLevelDisplay;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        ChangeGravityBasedOnScene(scene.name);
        ChangeTextLevelDisplay(scene.name);
    }

    public void ChangeTextLevelDisplay(string text)
    {
        textLevelDisplay.text = "You're on " + text + ", gravity is " + Physics.gravity.y + " ms";
    }

    public void PrintTextForLoading(string text)
    {
        textForLoading.text = text;
    }

    //Quando si cambierà livello cambierà anche la gravità
    private void ChangeGravityBasedOnScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Earth":
                Physics.gravity = new Vector3(0, -9.807F, 0);
                break;
            case "Mars":
                Physics.gravity = new Vector3(0, -3.721F, 0);
                break;
            case "Pluto":
                Physics.gravity = new Vector3(0, -0.62F, 0);
                break;
            case "Neptune":
                Physics.gravity = new Vector3(0, -11.15F, 0);
                break;
            case "Uranus":
                Physics.gravity = new Vector3(0, -8.87F, 0);
                break;
            case "Saturn":
                Physics.gravity = new Vector3(0, -10.44F, 0);
                break;
            case "Jupiter":
                Physics.gravity = new Vector3(0, -24.79F, 0);
                break;
        }
    }
}
