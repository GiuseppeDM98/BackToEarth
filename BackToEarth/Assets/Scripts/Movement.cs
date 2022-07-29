using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Scene scene;
    Rigidbody rb;
    [SerializeField] private float mainThrustForce = 1000;
    [SerializeField] private float rotationThrustForce = 100;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        ChangeGravityBasedOnScene(scene.name);
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() 
    {
        if(Input.GetKey(KeyCode.Space))
        {
            //scrivere Vector3.up è uguale a 0, 1, 0
            rb.AddRelativeForce(mainThrustForce * Time.deltaTime * Vector3.up);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //scrivere Vector3.forward è uguale a 0, 0, 1
            ApplyRotation(rotationThrustForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //scrivere Vector3.forward è uguale a 0, 0, -1
            ApplyRotation(-rotationThrustForce);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(rotationThisFrame * Time.deltaTime * Vector3.forward);
        rb.freezeRotation = false;
    }

    //Quando si cambierà livello idealmente cambierà anche la gravità
    private void ChangeGravityBasedOnScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Sandbox":
                Physics.gravity = new Vector3(0, -9.81F, 0);
                break;
        }
    }
}
