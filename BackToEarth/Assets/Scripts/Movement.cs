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
    [SerializeField] public float mainThrustForce = 2000f;
    [SerializeField] private float rotationThrustForce = 100f;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftSideBoosterParticles;
    [SerializeField] ParticleSystem rightSideBoosterParticles;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        collisionHandler = GetComponent<CollisionHandler>();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() 
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting(mainThrustForce, true);
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

    void StartThrusting(float forceApplied, bool playParticleEffects)
    {
        //scrivere Vector3.up è uguale a 0, 1, 0
        rb.AddRelativeForce(forceApplied * Time.deltaTime * Vector3.up);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticles.isPlaying && playParticleEffects)
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
        ApplyRotation(-rotationThrustForce);
        if (!leftSideBoosterParticles.isPlaying)
        {
            collisionHandler.PlayParticleEffects(leftSideBoosterParticles);
        }
    }

    private void RotateLeft()
    {
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
        //scrivere Vector3.forward è uguale a 0, 0, 1
        transform.Rotate(rotationThisFrame * Time.deltaTime * Vector3.forward);
        rb.freezeRotation = false;
    }
}
