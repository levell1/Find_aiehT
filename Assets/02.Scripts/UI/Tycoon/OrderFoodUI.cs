using UnityEngine;
using UnityEngine.UI;

public class OrderFoodUI : MonoBehaviour
{
    public Canvas OrderFoodCanvas;
    public Image BalloonBack;
    public Image BalloonGauge;
    public Image SelectFood;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        BalloonBack.fillAmount = 1f;
    }

    private void Update()
    {
        OrderFoodCanvas.transform.LookAt(OrderFoodCanvas.transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation* Vector3.up);
    }

    public void ActiveUI()
    {
        BalloonBack.gameObject.SetActive(true);
        BalloonGauge.gameObject.SetActive(true);
        SelectFood.gameObject.SetActive(true);
    }

    public void InactiveUI()
    {
        BalloonBack.gameObject.SetActive(false);
        BalloonGauge.gameObject.SetActive(false);
        SelectFood.gameObject.SetActive(false);
    }

}
