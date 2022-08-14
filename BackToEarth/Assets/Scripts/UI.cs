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
        ChangeTextLevelDisplay(scene.name);
    }

    public void ChangeTextLevelDisplay(string text)
    {
        textLevelDisplay.text = "You're on " + text + " gravity is " + Physics.gravity.y + " ms";
    }

    public void PrintTextForLoading(string text)
    {
        textForLoading.text = text;
    }
}
