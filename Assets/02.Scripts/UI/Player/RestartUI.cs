using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;

public class RestartUI : BaseUI
{
    [SerializeField] private Image _backImage;
    public TextMeshProUGUI Description;
    private float _duration = 2f;

    private void OnEnable()
    {
        _backImage.color = new Color(0, 0, 0, 0);
        Description.color = new Color(1, 1, 1, 0);

        ActiveUI();
    }

    private void ActiveUI()
    {
        _backImage.DOFade(1f, _duration);
        Description.DOFade(1f, _duration);
        StartCoroutine(GoVillage());
    }

    private IEnumerator GoVillage()
    {
        yield return new WaitForSeconds(_duration);
        GameManager.Instance.Player.transform.position = new Vector3(-5, 0, 0);
        LoadingSceneController.LoadScene(SceneName.VillageScene);
        gameObject.SetActive(false);
    }
}
