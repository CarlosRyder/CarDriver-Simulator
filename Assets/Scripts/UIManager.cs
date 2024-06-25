using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI collectablesText;
    public TextMeshProUGUI livesText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
    }

    public void UpdateLivesText(int lives)
    {
        livesText.text = "" + lives;
    }
}
