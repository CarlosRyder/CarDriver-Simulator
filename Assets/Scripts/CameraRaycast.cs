using UnityEngine;
using System.Collections.Generic;

public class CameraRaycast : MonoBehaviour
{
    public Transform player; 
    public LayerMask layerMask; 
    private List<Renderer> lastRenderers = new List<Renderer>(); 

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, direction.magnitude, layerMask);

        foreach (Renderer renderer in lastRenderers)
        {
            ResetTransparency(renderer);
        }

        lastRenderers.Clear();

        foreach (RaycastHit hit in hits)
        {
            Renderer hitRenderer = hit.collider.GetComponent<Renderer>();
            if (hitRenderer != null)
            {
                ChangeTransparency(hitRenderer);
                lastRenderers.Add(hitRenderer);
            }
        }
    }

    void ChangeTransparency(Renderer renderer)
    {
        Material material = renderer.material;
        Color color = material.color;
        color.a = 0.3f; 
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
        Material material = renderer.material;
        Color color = material.color;
        color.a = 1f; 
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
