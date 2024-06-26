using UnityEngine;

public class Collectable : MonoBehaviour
{
    public static int totalCollectables = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectTarget();
        }
    }

    private void CollectTarget()
    {
        totalCollectables++;
        Destroy(gameObject);
        UIManager.Instance.UpdateCollectablesText();
    }
}
