using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    static string NextScene;
    public GameObject DontObjects;

    [SerializeField] private Image _bar;
    [SerializeField] private Image _backImage;
    [SerializeField] private TMP_Text _percentText;
    [SerializeField] private TMP_Text _tipText;
    private int _randomIndex;

    private Color _color;

    private void Awake()
    {
        Time.timeScale = 1f;
        //TODO 태그로 변경
        DontObjects = FindObjectOfType<DontDestroy>().gameObject;
    }

    public static void LoadScene(string nextScene) 
    {
        NextScene = nextScene;
        if (NextScene==SceneName.HuntingScene)
        {
            GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Door);
        }
        if (NextScene == SceneName.TycoonScene)
        {
            GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.HouseDoor);
        }
        SceneManager.LoadScene(SceneName.LoadingScene);
    }
    
    private void Start()
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("CSV/Tip");
        _randomIndex = Random.Range(0, data_Dialog.Count);
        _tipText.text = (data_Dialog[_randomIndex]["Tip"].ToString());

        _backImage.gameObject.SetActive(true);
        _backImage.color = new Color(0f, 0f, 0f, 1f);
        _color = _backImage.color;
        StartCoroutine(LoadSceneProcess());

    }

    IEnumerator LoadSceneProcess() 
    {
        DontObjects.SetActive(false);

        while (_backImage.color.a > 0.1f)
        {
            yield return null;
            _color.a = _color.a - 0.01f;
            _backImage.color = _color;
        }
        yield return new WaitForSeconds(0.2f);

        AsyncOperation op = SceneManager.LoadSceneAsync(NextScene);
        op.allowSceneActivation = false; 
        float timer = 0f;
        while(!op.isDone) 
        {
            yield return null;
            _percentText.text = ((int)(_bar.fillAmount * 100)).ToString() + "%";
            if (op.progress <0.9f)
            {
                _bar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime; 
                _bar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (_bar.fillAmount >= 1f)
                {
                    while (_backImage.color.a < 1f)
                    {
                        yield return null;
                        _color.a = _color.a + 0.01f;
                        _backImage.color = _color;
                    }
                    op.allowSceneActivation = true;
                    op.completed += c => { DontObjects.SetActive(true); };
                    yield break;
                }
            }
            
        }
    }
}
