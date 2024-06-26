using UnityEngine;

public class CarController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    float movX, movZ;
    public float degrees, speed;
    Vector3 movement;
    private AudioSource carAudioSource;
    public AudioClip carDrivingClip;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        carAudioSource = GetComponent<AudioSource>();

        if (carAudioSource == null)
        {
            Debug.LogWarning("No AudioSource component found on this GameObject.");
        }
        else
        {
            if (carAudioSource.clip == null)
            {
                carAudioSource.clip = carDrivingClip;
            }
        }
    }

    void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");
        movement = transform.forward * movZ;
    }

    private void FixedUpdate()
    {
        bool isMoving = movX != 0 || movZ != 0;

        if (isMoving)
        {
            playerRigidbody.MovePosition(transform.position + movement * Time.deltaTime * speed);
            Quaternion turnRotation = Quaternion.Euler(0f, movX * degrees * Time.deltaTime, 0f);
            playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);

            if (!carAudioSource.isPlaying)
            {
                carAudioSource.Play();
            }
        }
        else
        {
            if (carAudioSource.isPlaying)
            {
                carAudioSource.Stop();
            }
        }
    }
}
