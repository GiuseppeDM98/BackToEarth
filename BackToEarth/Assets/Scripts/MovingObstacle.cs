using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    //Per aumentare la velocità bisogna diminuire questo valore
    [SerializeField] float period = 0f;
    public float rotationForce;
    public float rotationAngleX;
    public float rotationAngleY;
    public float rotationAngleZ;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveObstacle();
        RotateObstacle();
    }

    private void MoveObstacle()
    {
        //In questo modo se period = 0 non ritorna l'errore NaN, in quanto staremmo dividendo un valore per zero e questo non è bello
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;

        //Valore costante di 6.283
        const float tau = Mathf.PI * 2;
        //Ritorna un valore compreso tra -1 e 1
        float rawSinWave = Mathf.Sin(cycles * tau);

        //Ricalcolato per andare da 0 ad 1, per aumentare il range bisogna modificare il +1f ed utilizzare un altro valore
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }

    private void RotateObstacle()
    {
        transform.Rotate((rotationAngleX * rotationForce * Time.deltaTime), (rotationAngleY * rotationForce * Time.deltaTime), (rotationAngleZ * rotationForce * Time.deltaTime));
    }
}
