using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSystem : MonoBehaviour
{
    public GameObject TakeDamageText;
    public GameObject TakeDamageText2;

    private Player _player;
    private PlayerExpSystem _playerExpSystem;
    private Enemy _enemy;
    private EnemySO _enemySO;
    public float MaxHealth;
    public float Health;
    public Canvas HpCanvas;
    public Image HpBack;
    public Image HpBar;
    public TextMeshProUGUI EnemyName;
    public bool Hit;
    public int HitCool;
    public float DamageAmount;
    public event Action OnDie;
    public static event Action<int> OnQuestTargetDie;
    public static event Action<int> OnMainQuestTargetDie;

    public bool IsDead;

    private Camera _camera;

    private SkinnedMeshRenderer[] meshRenderers;

    Coroutine _coroutine;

    private void Start()
    {
        _player = GameManager.Instance.Player.GetComponent<Player>();
        _playerExpSystem = GameManager.Instance.Player.GetComponent<PlayerExpSystem>();
        _enemy = GetComponent<Enemy>();
        _enemySO = GetComponent<Enemy>().Data;
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        MaxHealth = _enemy.EnemyMaxHealth;
        Health = MaxHealth;
        EnemyNameToAppropriateColor();
        _playerExpSystem.OnChangeEnemyName += EnemyNameToAppropriateColor;
        HpBack.gameObject.SetActive(false);
        _camera = Camera.main;
    }

    private void Update()
    {
        HpCanvas.transform.LookAt(HpCanvas.transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);

        if (IsAcitveNameRange())
        {
            HpCanvas.gameObject.SetActive(true);
        }
        else
        {
            HpCanvas.gameObject.SetActive(false);
        }

        if (Hit)
        {
            HpBack.gameObject.SetActive(true);
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(HitCancel());
            }
        }
        else
        {
            HpBack.gameObject.SetActive(false);
        }

        HpBar.fillAmount = Health / MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        Hit = true;
        if (Health == 0) return;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        DamageAmount = damage;
        if (TakeDamageText.activeSelf)
        {
            TakeDamageText2.SetActive(true);
        }
        TakeDamageText.SetActive(true);

        Health = Mathf.Max(Health - damage, 0);
        StartCoroutine(DamageFlash());

        _enemy.StateMachine.ChangeState(_enemy.StateMachine.ChasingState);

        if (Health == 0)
        {
            int bossRooster = 3901;
            if(_enemySO.EnemyID == bossRooster)
            {
                int questID = GameManager.Instance.DataManager.QuestDataList.MainQuestData[2].QuestID;
                OnMainQuestTargetDie?.Invoke(questID);
            }

            IsDead = true;
            OnDie.Invoke();
            OnQuestTargetDie?.Invoke(_enemySO.EnemyID);
        }
    }

    private IEnumerator HitCancel()
    {
        yield return new WaitForSeconds(HitCool); //해당시간동안 플레이어를 추격한다.
        Hit = false;
        _coroutine = null;
    }

    public IEnumerator DamageFlash()
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        Color a = meshRenderers[0].material.color;
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            
            meshRenderers[i].GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", new Color(1.0f, 0.4f, 0.4f));
            meshRenderers[i].SetPropertyBlock(propBlock);
        }

        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", a);
            meshRenderers[i].SetPropertyBlock(propBlock);
        }
    }

    public void EnemyNameToAppropriateColor()
    {
        int randomLevel = UnityEngine.Random.Range(0, _enemySO.AppropriateLevel.Length);
        EnemyName.text = string.Format("[ LV." + _enemySO.AppropriateLevel[randomLevel] + " " + _enemySO.EnemyName + " ]");

        for (int i = 0;i < _enemySO.AppropriateLevel.Length; ++i)
        {
            if (_player.Data.PlayerData.PlayerLevel >= _enemySO.AppropriateLevel[i])
            {
                EnemyName.color = Color.white;
            }
            else if (_player.Data.PlayerData.PlayerLevel < _enemySO.AppropriateLevel[i]) // 별로 차이 안날 때
            {
                EnemyName.color = Color.yellow;
                if (_enemySO.AppropriateLevel[0] - _player.Data.PlayerData.PlayerLevel >= 3) // 많이 차이 날 때
                {
                    EnemyName.color = Color.red;
                }

            }
        }
    }

    private bool IsAcitveNameRange()
    {
        float playerDistanceSqr = (_player.transform.position - _enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= _enemy.Data.ActiveNameRange * _enemy.Data.ActiveNameRange;
    }
}
