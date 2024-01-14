using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)] //인스펙터 창에서 0~1 스크롤로 조절 가능
    public float DayTime;
    public float FullDayLength;  //하루
    public float StartTime = 0.4f;
    private float _timeRate;
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

    private void Start()
    {
        _timeRate = 1.0f / FullDayLength; //얼마큼씩 변하는지 계산 1/하루
        DayTime = StartTime; //시작시간을 정해서 아침부터 시작하는 느낌쓰 데이터를 저장하면 필요 없을 듯
    }

    private void Update()
    {
        DayTime = (DayTime + _timeRate * Time.deltaTime) % 1.0f; //퍼센티지로 사용하기 위해 1.0f로 나눈다. 0 ~ 0.9999 까지만 사용가능
        UpdateLighting(Sun, SunColor, SunIntensity);
        UpdateLighting(Moon, MoonColor, MoonIntensity);

        RenderSettings.ambientIntensity = LightingIntensityMultiplier.Evaluate(DayTime);
        RenderSettings.reflectionIntensity = ReflectionIntensityMultiplier.Evaluate(DayTime);
    }

    private void UpdateLighting(Light lightSource, Gradient colorGradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(DayTime);  //AnimationCurve.Evaluate(*)는 *의 값의 커브값을 리턴해준다. (그래프)

        lightSource.transform.eulerAngles = (DayTime - ((lightSource == Sun) ? 0.25f : 0.75f)) * Noon * 4.0f;
        lightSource.color = colorGradient.Evaluate(DayTime);
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
