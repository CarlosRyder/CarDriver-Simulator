using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float initialTime = 60f; 
    private float currentTime;
    public TextMeshProUGUI timerText; 
    private bool timerIsRunning = false;
    public GameObject[] objectsToActivateOnTimerZero; 
    public GameObject[] objectsToDeactivateOnTimerZero;
    private bool timerActive;

    void Start()
    {
        currentTime = initialTime;
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerDisplay(currentTime);
            }
            else
            {
                currentTime = 0;
                timerIsRunning = false;
                OnTimerEnd();
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay += 1; 

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerEnd()
    {
        timerActive = false;

        foreach (GameObject obj in objectsToActivateOnTimerZero)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in objectsToDeactivateOnTimerZero)
        {
            obj.SetActive(false);
        }
    }
}
