using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinimapSetting : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Camera _minimapCamera;
    [SerializeField] private float _zoomMin = 1;
    [SerializeField] private float _zoomMax = 30;
    [SerializeField] private TMP_Text _mapName;

    private void Awake()
    {
        _player = GameManager.Instance.Player.transform;
    }
    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode arg1)
    {
        if (scene.name == SceneName.VillageScene)
        {
            ChangeMapName("마을");
        }
        else if (scene.name == SceneName.Field)
        {
            ChangeMapName("사냥터");
        }
        else if (scene.name == "Doungeon")
        {
            ChangeMapName("던전");
        }
        else
        {
            ChangeMapName("테스트");
        }
    }

    private void Update()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y + 10, _player.position.z);
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            ZoomIn();
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            ZoomOut();
        }
    }
    
    // 플레이어가 사냥터 어느 지역 가면 changeMapName, 소리변경

    public void ChangeMapName(string mapname) 
    {
        //FIX
        //소리변경시, 맵 트리거 시 바뀌게 수정
        _mapName.text = mapname;
    }

    public void ZoomIn() 
    {
        _minimapCamera.orthographicSize = Mathf.Max(_minimapCamera.orthographicSize - 1, _zoomMin);
    }
    public void ZoomOut()
    {
        _minimapCamera.orthographicSize = Mathf.Min(_minimapCamera.orthographicSize + 1, _zoomMax);
    }
}
