using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //Datos requeridos
    Rigidbody playerRigidbody;
    float movX, movZ;
    public float degrees, speed;
    //public int points;
    Vector3 movement;

    void Start()
    {
        //Referencia Componente RigidBody
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Guardamos los inputs en MovX y movZ
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");

        //En este Vector3 Decimos que almacene el valor y lo asigne al eje Z
        movement = transform.forward * movZ;
    }
    private void FixedUpdate()
    {

        //Si estoy presionando los botones de movimiento
        if (movX != 0 || movZ != 0)
        {
            //Muevo a mi personaje de manera frontal
            playerRigidbody.MovePosition(transform.position + movement * Time.deltaTime * speed);
            //Roto para poder dirigirlo
            Quaternion turnRotation = Quaternion.Euler(0f, movX * degrees * Time.deltaTime, 0f);
            playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);
        }
    }
}