using UnityEngine;

public class CarController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    float movX, movZ;
    public float degrees, speed;
    Vector3 movement;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");
        movement = transform.forward * movZ;
    } 

    private void FixedUpdate()
    {
        if (movX != 0 || movZ != 0)
        {
            playerRigidbody.MovePosition(transform.position + movement * Time.deltaTime * speed);
            Quaternion turnRotation = Quaternion.Euler(0f, movX * degrees * Time.deltaTime, 0f);
            playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);
        }
    }
}