using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI : MonoBehaviour
{
    Scene scene;
    CollisionHandler collisionHandler;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI textDisplay;
    [SerializeField] TextMeshProUGUI textLevelDisplay;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        ChangeTextLevelDisplay(scene.name);
        collisionHandler = player.GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(collisionHandler.isTransitioning)
        {
            textDisplay.text = collisionHandler.messageForLoading;
        }
    }

    public void ChangeTextLevelDisplay(string text)
    {
        textLevelDisplay.text = "You're on " + text;
    }
}
