using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Enum
{
    ITEM,
    NPC
}

public class PlayerInteraction : MonoBehaviour
{
    private GlobalTimeManager _globalTimeManager;

    public TMP_Text InteractionText; 
    public TextMeshProUGUI ErrorText;

    public LayerMask LayerMask;

    private Collider _interactCollider;
    [SerializeField] private Collider _playerCollider;

    private List<string> _interactionLayerList = new List<string>();

    private List<ItemObject> _interactItemObejctList = new List<ItemObject>();

    private Dictionary<int, Enum> LayerDic = new Dictionary<int, Enum>();

    private MoveSceneController _moveSceneController;
    private string _nextScene = string.Empty;
    private string _curScene = string.Empty;
    private string _showUI = string.Empty;

    public GameObject ShopUI;

    Coroutine _coroutine;

    private void Awake()
    {
        _globalTimeManager = GameManager.Instance.GlobalTimeManager;
    }

    private void Start()
    {
        InitializeCollider();

        _interactCollider = GetComponent<Collider>();

        LayerDic.Add(LayerMask.NameToLayer(LayerName.Item), Enum.ITEM);
        LayerDic.Add(LayerMask.NameToLayer(LayerName.NpcInteract), Enum.NPC);

        SceneManager.sceneLoaded += LoadSceneEvent;
    }

    private void LoadSceneEvent(Scene scene, LoadSceneMode mode)
    {
        _curScene = scene.name;
        InteractionText.text = string.Empty;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other == _playerCollider) { return; }

        if (other == _interactCollider) { return; }


        if (LayerDic.TryGetValue(other.gameObject.layer, out Enum _objectType))
        {
            if (_objectType == Enum.ITEM)
            {
                ItemObject itemObject = other.gameObject.GetComponent<ItemObject>();

                _interactItemObejctList.Add(itemObject);

                _interactionLayerList.Add(itemObject.ItemData.ObjName);
                UpdateUI();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (LayerDic.TryGetValue(other.gameObject.layer, out Enum _objectType))
        {
            if (_objectType == Enum.NPC)
            {
                if (other.gameObject.CompareTag(TagName.RealDoor))
                {
                    _moveSceneController = other.gameObject.GetComponent<MoveSceneController>();
                    _nextScene = _moveSceneController.NextScene;

                    InteractionText.text = _moveSceneController.NextSceneInfo;
                }
                else if (other.gameObject.CompareTag(TagName.PotionShop)) 
                {
                    InteractionText.text = "포션상점";
                    _showUI = UIName.ShopUI;
                }
                else if (other.gameObject.CompareTag(TagName.Enhancement)) 
                {
                    InteractionText.text = "강화소";
                    _showUI = UIName.ReforgeUI;
                }
                else if (other.gameObject.CompareTag(TagName.QuestNPC)) 
                {
                    InteractionText.text = "퀘스트";
                    _showUI = UIName.QuestUI;
                }
                else if (other.gameObject.CompareTag(TagName.DungeonNPC)) 
                {
                    InteractionText.text = "던전 입장";
                    _showUI = UIName.DungeontUI;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ItemObject itemObject = other.gameObject.GetComponent<ItemObject>();

        if (itemObject != null)
        {
            _interactionLayerList.Remove(itemObject.ItemData.ObjName);
            _interactItemObejctList.Remove(itemObject);
            UpdateUI(); 
        }

        if (_nextScene != null)
        {
            _nextScene = string.Empty;
        }

        if (_showUI != null)
        {
            _showUI = string.Empty;
        }

        InteractionText.text = string.Empty;
    }

    private void InitializeCollider()
    {
        _interactionLayerList.Clear();
        UpdateUI();
    }



    private void UpdateUI()
    {
        if (InteractionText != null)
        {
            InteractionText.text = "";

            foreach (var item in _interactionLayerList)
            {
                InteractionText.text += "- " + item + "\n";
            }
        }
    }

    public void DestroyItem()
    {
        if (_interactItemObejctList.Count > 0)
        {
            ItemObject itemObject = _interactItemObejctList[0];

            _interactItemObejctList.Remove(itemObject);
            _interactionLayerList.Remove(itemObject.ItemData.ObjName);

            itemObject.GetItem();
            UpdateUI();
        }
    }

    public void GoNextScene()
    {
        if (_nextScene != string.Empty)
        {
            if (_nextScene == SceneName.TycoonScene && !_globalTimeManager.EnterTycoonTime())
            {
                if(_coroutine == null)
                {
                    _coroutine = StartCoroutine(ErrorMessage());
                }
                return;
            }

            if (_curScene == SceneName.DungeonScene && _nextScene == SceneName.VillageScene)
            {
                GameManager.Instance.GlobalTimeManager.GoodMorning();
                return;
            }
            if (_curScene == SceneName.VillageScene && _nextScene == SceneName.VillageScene)
            {
                GameManager.Instance.GlobalTimeManager.GoodMorning();
                return;
            }

            if (_nextScene == SceneName.TycoonScene)
            {
                GameManager.Instance.Player.transform.position = new Vector3(-4, 0, 8);
            }
            else if (_curScene == SceneName.HuntingScene && _nextScene == SceneName.VillageScene)
            {
                if (_globalTimeManager.IsMoveFieldToVillage)
                {
                    GameManager.Instance.GlobalTimeManager.GoodMorning();
                    return;
                }

                GameManager.Instance.Player.transform.position = new Vector3(-4, 0, 19);

            }

            LoadingSceneController.LoadScene(_nextScene);
            _nextScene = string.Empty;

            InteractionText.text = string.Empty;
        }
    }

    public void ShowUI()
    {
        if (_showUI != string.Empty)
        {
            GameManager.Instance.UIManager.ShowCanvas(_showUI);
            InteractionText.text = string.Empty;
        }
    }

    public IEnumerator ErrorMessage()
    {
        if(_nextScene == SceneName.TycoonScene || _nextScene == SceneName.HuntingScene)
        {
            ErrorText.text = ErrorMessageTxt.DontSceneMoveErrorMessage;
        }

        ErrorText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        ErrorText.gameObject.SetActive(false);
        ErrorText.text = string.Empty;

        _coroutine = null;
    }
}
