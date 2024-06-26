using UnityEngine;

public class Collectable : MonoBehaviour
{
    public static int totalCollectables = 0;
    private AudioSource audioSource;
    private bool isCollecting = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectTarget();
        }
    }

    private void CollectTarget()
    {
        if (isCollecting)
        {
            return;
        }

        isCollecting = true;
        totalCollectables++;
        UIManager.Instance.UpdateCollectablesText();

        if (audioSource != null)
        {
            audioSource.Play();
        }

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Invoke("DeactivateObject", audioSource.clip.length);
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
