using UnityEngine;
using System.Collections.Generic;

public class CameraRaycast : MonoBehaviour
{
    public Transform player;
    public LayerMask obstacleLayer;

    private Dictionary<Renderer, List<Material>> originalMaterials = new Dictionary<Renderer, List<Material>>();
    private List<Renderer> currentTransparentRenderers = new List<Renderer>();

    private void Update()
    {
        HandleObstacles();
    }

    private void HandleObstacles()
    {
        Vector3 direction = (player.position - Camera.main.transform.position).normalized;
        float distance = Vector3.Distance(player.position, Camera.main.transform.position);

        RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, direction, distance, obstacleLayer);
        List<Renderer> newTransparentRenderers = new List<Renderer>();

        foreach (RaycastHit hit in hits)
        {
            Renderer objectRenderer = hit.collider.gameObject.GetComponent<Renderer>();

            if (objectRenderer != null)
            {
                newTransparentRenderers.Add(objectRenderer);
                SetTransparent(objectRenderer);
            }
        }

        foreach (Renderer renderer in currentTransparentRenderers)
        {
            if (!newTransparentRenderers.Contains(renderer))
            {
                RestoreOriginalMaterial(renderer);
            }
        }

        currentTransparentRenderers = newTransparentRenderers;
    }

    private void SetTransparent(Renderer renderer)
    {
        if (!originalMaterials.ContainsKey(renderer))
        {
            List<Material> originalMats = new List<Material>();
            foreach (Material mat in renderer.materials)
            {
                Material originalMat = new Material(mat);
                originalMats.Add(originalMat);
            }
            originalMaterials.Add(renderer, originalMats);
        }

        foreach (Material mat in renderer.materials)
        {
            mat.SetFloat("_Mode", 3); // Set rendering mode to transparent
            Color color = mat.color;
            color.a = 0.3f; // Set alpha to 30%
            mat.color = color;

            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
    }

    private void RestoreOriginalMaterial(Renderer renderer)
    {
        if (originalMaterials.ContainsKey(renderer))
        {
            Material[] originalMats = originalMaterials[renderer].ToArray();
            renderer.materials = originalMats;

            foreach (Material mat in renderer.materials)
            {
                mat.SetFloat("_Mode", 0); // Set rendering mode to opaque
                Color color = mat.color;
                color.a = 1f; // Set alpha to 100%
                mat.color = color;

                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = -1;
            }
        }
    }
}
