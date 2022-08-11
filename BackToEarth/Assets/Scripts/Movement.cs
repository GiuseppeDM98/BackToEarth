using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Scene scene;
    Rigidbody rb;
    AudioSource audioSource;
    CollisionHandler collisionHandler;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] private float mainThrustForce = 1000f;
    [SerializeField] private float rotationThrustForce = 100f;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftSideBoosterParticles;
    [SerializeField] ParticleSystem rightSideBoosterParticles;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        ChangeGravityBasedOnScene(scene.name);
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        collisionHandler = GetComponent<CollisionHandler>();
    }


    // Update is called once per frame
    /*void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }*/

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() 
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        //scrivere Vector3.up è uguale a 0, 1, 0
        rb.AddRelativeForce(mainThrustForce * Time.deltaTime * Vector3.up);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticles.isPlaying)
        {
            collisionHandler.PlayParticleEffects(mainBoosterParticles);
        }
    }

    //stoppa l'audio e il particle effects del main booster
    public void StopThrusting()
    {
        audioSource.Stop();
        collisionHandler.StopParticleEffects(mainBoosterParticles);
    }


    private void RotateRight()
    {
        //scrivere Vector3.forward è uguale a 0, 0, -1
        ApplyRotation(-rotationThrustForce);
        if (!leftSideBoosterParticles.isPlaying)
        {
            collisionHandler.PlayParticleEffects(leftSideBoosterParticles);
        }
    }

    private void RotateLeft()
    {
        //scrivere Vector3.forward è uguale a 0, 0, 1
        ApplyRotation(rotationThrustForce);
        if (!rightSideBoosterParticles.isPlaying)
        {
            collisionHandler.PlayParticleEffects(rightSideBoosterParticles);
        }
    }

    //stoppa i particle effects dei booster laterali
    public void StopRotating()
    {
        collisionHandler.StopParticleEffects(rightSideBoosterParticles);
        collisionHandler.StopParticleEffects(leftSideBoosterParticles);
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
            case "EarthLevel":
                Physics.gravity = new Vector3(0, -9.807F, 0);
                break;
            case "MarsLevel":
                Physics.gravity = new Vector3(0, -3.721F, 0);
                break;
        }
    }
}
