using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float RotationPerSecond = 1;
    public bool rotate;

    protected void Update()
    {
        if (rotate) RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationPerSecond);
    }

    public void ToggleSkyboxRotation()
    {
        rotate = !rotate;
    }
}