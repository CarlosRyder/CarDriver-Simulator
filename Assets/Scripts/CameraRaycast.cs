using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public Transform player; 
    public LayerMask layerMask; 
    private Renderer lastRenderer; 

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, direction.magnitude, layerMask))
        {
            Renderer hitRenderer = hit.collider.GetComponent<Renderer>();

            if (hitRenderer != null)
            {
                if (lastRenderer != null && lastRenderer != hitRenderer)
                {
                    ResetTransparency(lastRenderer);
                }
                ChangeTransparency(hitRenderer);
                lastRenderer = hitRenderer;
            }
        }
        else
        {
            if (lastRenderer != null)
            {
                ResetTransparency(lastRenderer);
                lastRenderer = null;
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

