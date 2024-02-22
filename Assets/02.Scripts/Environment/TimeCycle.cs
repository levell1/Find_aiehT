using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    public Vector3 Noon;  

    [Header("Sun")]
    public Light Sun;
    public Gradient SunColor;
    public AnimationCurve SunIntensity;

    [Header("Moon")]
    public Light Moon;
    public Gradient MoonColor;
    public AnimationCurve MoonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve LightingIntensityMultiplier;  
    public AnimationCurve ReflectionIntensityMultiplier;  

    

    private void Update()
    {
        UpdateLighting(Sun, SunColor, SunIntensity);
        UpdateLighting(Moon, MoonColor, MoonIntensity);

        RenderSettings.ambientIntensity = LightingIntensityMultiplier.Evaluate(GameManager.Instance.GlobalTimeManager.DayTime);
        RenderSettings.reflectionIntensity = ReflectionIntensityMultiplier.Evaluate(GameManager.Instance.GlobalTimeManager.DayTime);
    }

    private void UpdateLighting(Light lightSource, Gradient colorGradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(GameManager.Instance.GlobalTimeManager.DayTime);  

        lightSource.transform.eulerAngles = (GameManager.Instance.GlobalTimeManager.DayTime - ((lightSource == Sun) ? 0.25f : 0.75f)) * Noon * 4.0f;
        lightSource.color = colorGradient.Evaluate(GameManager.Instance.GlobalTimeManager.DayTime);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject;
        if (lightSource.intensity == 0 && go.activeInHierarchy)
        {
            go.SetActive(false);
        }
        else if (lightSource.intensity > 0 && !go.activeInHierarchy)
        {
            go.SetActive(true);
        }
    }
}
