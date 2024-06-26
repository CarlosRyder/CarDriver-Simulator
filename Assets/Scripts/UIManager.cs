using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI collectablesText;
    public TextMeshProUGUI livesText;
    public GameObject[] objectsToActivateOnCollectables10;
    public GameObject[] objectsToDeactivateOnCollectables10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCollectablesText();
    }

    public void UpdateCollectablesText()
    {
        collectablesText.text = " " + Collectable.totalCollectables;

        if (Collectable.totalCollectables >= 10)
        {
            OnCollectables10();
        }
    }

    private void OnCollectables10()
    {

        foreach (GameObject obj in objectsToActivateOnCollectables10)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in objectsToDeactivateOnCollectables10)
        {
            obj.SetActive(false);
        }
    }

    public void UpdateLivesText(int lives)
    {
        livesText.text = "" + lives;
    }
}

