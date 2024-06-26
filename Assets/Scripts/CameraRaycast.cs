using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public LayerMask layerMask; // Capa que incluye los objetos que pueden volverse transparentes
    private Renderer lastRenderer; // Para almacenar el último objeto detectado

    void Update()
    {
        // Dirección desde la cámara hacia el jugador
        Vector3 direction = player.position - transform.position;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        // Realiza el raycast
        if (Physics.Raycast(ray, out hit, direction.magnitude, layerMask))
        {
            // Si golpea un objeto
            Renderer hitRenderer = hit.collider.GetComponent<Renderer>();

            if (hitRenderer != null)
            {
                // Si hay un objeto anterior, restablece su transparencia
                if (lastRenderer != null && lastRenderer != hitRenderer)
                {
                    ResetTransparency(lastRenderer);
                }

                // Cambia la transparencia del objeto golpeado
                ChangeTransparency(hitRenderer);
                lastRenderer = hitRenderer;
            }
        }
        else
        {
            // Si no golpea ningún objeto, restablece la transparencia del último objeto
            if (lastRenderer != null)
            {
                ResetTransparency(lastRenderer);
                lastRenderer = null;
            }
        }
    }

    void ChangeTransparency(Renderer renderer)
    {
        // Cambia la transparencia del objeto
        Material material = renderer.material;
        Color color = material.color;
        color.a = 0.3f; // Cambia la transparencia (0 es totalmente transparente, 1 es opaco)
        material.color = color;
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    void ResetTransparency(Renderer renderer)
    {
        // Restablece la transparencia del objeto
        Material material = renderer.material;
        Color color = material.color;
        color.a = 1f; // Restablece la opacidad
        material.color = color;
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
    }
}

