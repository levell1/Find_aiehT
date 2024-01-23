using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    public Vector3 Noon;  //빛의 각도 조절

    [Header("Sun")]
    public Light Sun;
    public Gradient SunColor;
    public AnimationCurve SunIntensity;

    [Header("Moon")]
    public Light Moon;
    public Gradient MoonColor;
    public AnimationCurve MoonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve LightingIntensityMultiplier;  //환경광
    public AnimationCurve ReflectionIntensityMultiplier;  //반사광

    

    private void Update()
    {
        UpdateLighting(Sun, SunColor, SunIntensity);
        UpdateLighting(Moon, MoonColor, MoonIntensity);

        RenderSettings.ambientIntensity = LightingIntensityMultiplier.Evaluate(GameManager.instance.GlobalTimeManager.DayTime);
        RenderSettings.reflectionIntensity = ReflectionIntensityMultiplier.Evaluate(GameManager.instance.GlobalTimeManager.DayTime);
    }

    private void UpdateLighting(Light lightSource, Gradient colorGradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(GameManager.instance.GlobalTimeManager.DayTime);  //AnimationCurve.Evaluate(*)는 *의 값의 커브값을 리턴해준다. (그래프)

        lightSource.transform.eulerAngles = (GameManager.instance.GlobalTimeManager.DayTime - ((lightSource == Sun) ? 0.25f : 0.75f)) * Noon * 4.0f;
        lightSource.color = colorGradient.Evaluate(GameManager.instance.GlobalTimeManager.DayTime);
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
