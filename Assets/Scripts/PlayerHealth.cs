using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    public GameObject[] objectsToActivateOnZeroLives;
    public GameObject[] objectsToDeactivateOnZeroLives;

    private void Start()
    {
        currentLives = maxLives;
        UIManager.Instance.UpdateLivesText(currentLives);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
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
        Debug.Log("Game Over");

        foreach (GameObject obj in objectsToActivateOnZeroLives)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in objectsToDeactivateOnZeroLives)
        {
            obj.SetActive(false);
        }
    }
}
