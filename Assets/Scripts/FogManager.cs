using UnityEngine;

public class FogFade : MonoBehaviour
{
    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.gray7;
    }
}
