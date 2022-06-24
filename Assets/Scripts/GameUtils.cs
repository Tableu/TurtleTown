using UnityEngine;

public class GameUtils
{
    public static void SetAllRenderers(GameObject gameObject, bool enable)
    {
        if (gameObject == null)
        {
            return;
        }
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = enable;
        }
    }
}
