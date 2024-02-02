using System.Collections;
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
    [SerializeField] private TMP_Text _text;

    private Color _color;


    private void Awake()
    {
        DontObjects = FindObjectOfType<DontDestroy>().gameObject;
    }

    public static void LoadScene(string nextScene) 
    {
        NextScene = nextScene;
        SceneManager.LoadScene(SceneName.LoadingScene);
    }
    
    private void Start()
    {
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
        op.allowSceneActivation = false; // 씬의 로딩이 끝나면 자동으로 넘어갈지 설정 (false -> 90퍼에서 기다리고 진행)(로딩씬의 내용이 좀 더 보이게)
        float timer = 0f;
        while(!op.isDone) 
        {
            yield return null;
            _text.text = ((int)(_bar.fillAmount * 100)).ToString() + "%";
            if (op.progress <0.9f)
            {
                _bar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime; //TimeScale에 영향을 받지않는 DeltaTime
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
