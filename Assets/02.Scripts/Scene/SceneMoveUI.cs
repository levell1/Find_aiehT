using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneMoveUI : MonoBehaviour
{
    [SerializeField] private Image _backImage;
    public TextMeshProUGUI Description;
    public string CurrentSceneName;
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
        StartCoroutine(MoveScene());
    }

    private IEnumerator MoveScene()
    {
        yield return new WaitForSeconds(_duration);
        if (CurrentSceneName == SceneName.VillageScene)
        {
            GameManager.Instance.Player.transform.position = new Vector3(-5, 0, 0);
        }
        LoadingSceneController.LoadScene(CurrentSceneName);
        gameObject.SetActive(false);
    }

    
}
