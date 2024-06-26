using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    public GameObject[] objectsToActivateOnZeroLives;
    public GameObject[] objectsToDeactivateOnZeroLives;
    private AudioSource carAudioSource;
    public AudioClip carCrashingClip;

    private void Start()
    {
        currentLives = maxLives;
        UIManager.Instance.UpdateLivesText(currentLives);
        carAudioSource = GetComponent<AudioSource>();
        if (carAudioSource == null)
        {
            carAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            PlayCarCrashingSound();
            LoseLife();
        }
    }

    private void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            UIManager.Instance.UpdateLivesText(currentLives);

            if (currentLives <= 0)
            {
                OnZeroLives();
            }
        }
    }

    private void OnZeroLives()
    {
        foreach (GameObject obj in objectsToActivateOnZeroLives)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Object reference is null.");
            }
        }

        foreach (GameObject obj in objectsToDeactivateOnZeroLives)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Object reference is null.");
            }
        }
    }

    private void PlayCarCrashingSound()
    {
        if (carAudioSource != null && carCrashingClip != null)
        {
            carAudioSource.PlayOneShot(carCrashingClip);
        }
    }
}
