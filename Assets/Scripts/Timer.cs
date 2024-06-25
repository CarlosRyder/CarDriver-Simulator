using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float initialTime = 60f; // Tiempo inicial en segundos
    private float currentTime;
    public TextMeshProUGUI timerText; // Referencia al componente de texto de UI para mostrar el tiempo
    private bool timerIsRunning = false;

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
                Debug.Log("Time has run out!");
                currentTime = 0;
                timerIsRunning = false;
                OnTimerEnd();
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay += 1; // Para redondear el tiempo correctamente

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerEnd()
    {
        Debug.Log("Timer has ended.");
    }
}
