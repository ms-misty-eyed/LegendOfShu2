using UnityEngine;

public class FogFade : MonoBehaviour
{
    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.6784314f,0.6941177f,0.7058824f,1f);
    }
}
