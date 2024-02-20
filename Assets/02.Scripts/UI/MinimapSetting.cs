using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinimapSetting : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Camera _minimapCamera;
    [SerializeField] private float _zoomMin;
    [SerializeField] private float _zoomMax;
    [SerializeField] private TMP_Text _mapName;
    private float _camY = 0;
    private void Awake()
    {
        _player = GameManager.Instance.Player.transform;
        _minimapCamera.orthographicSize = 25;
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
        else if (scene.name == SceneName.HuntingScene)
        {
            ChangeMapName("사냥터");
        }
        else if (scene.name == SceneName.DungeonScene)
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
        _camY = GameManager.Instance.CameraManager.MainCamera.transform.rotation.eulerAngles.y;
        transform.position = new Vector3(_player.position.x, _player.position.y + 50, _player.position.z);
        transform.rotation = Quaternion.Euler(90f, _camY, 0f);
       
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
