using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

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
                Debug.Log("Game Over");
            }
        }
    }
}
