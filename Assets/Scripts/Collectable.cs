using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Referencia al contador de objetos recolectados
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
        // Incrementa el contador de objetos recolectados
        totalCollectables++;

        // Destruye el objeto recolectado
        Destroy(gameObject);

        UIManager.Instance.UpdateCollectablesText();
    }
}
