using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    CollisionHandler collisionHandler;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI textDisplay;

    // Start is called before the first frame update
    void Start()
    {
        collisionHandler = player.GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(collisionHandler.showMessage)
        {
            textDisplay.text = collisionHandler.messageForLoading;
        }
    }

    //Aggiungi un countdown per completare il livello, la variabile dovrà essere public!
}
