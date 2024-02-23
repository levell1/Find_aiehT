using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossManager : MonoBehaviour
{
    [SerializeField] private BossHealthSystem _greenpigHealthSystem;
    private BossHealthSystem _aightHealthSystem;
    [SerializeField] private Light _light;
    [SerializeField] private AiehtAI _aieht;
    [SerializeField] private GameObject _pigs;
    [SerializeField] private GameObject _endingUI;

    private void Awake()
    {
        _aightHealthSystem = _aieht.GetComponent<BossHealthSystem>();
        _greenpigHealthSystem.OnDie += RespawnNextBoss;

        _aightHealthSystem.OnDie += Ending;
    }

    private void Ending()
    {
        _endingUI.SetActive(true);
        GameManager.Instance.SoundManager.BgmSoundPlay(BGMSoundName.EndingBGM);
        _aightHealthSystem.OnDie -= Ending;
    }

    private void RespawnNextBoss()
    {
        StartCoroutine(BossEffect());
    }

    private IEnumerator BossEffect() 
    {
        var pigsAgent = _pigs.GetComponentsInChildren<NavMeshAgent>();
        GameManager.Instance.SoundManager.BgmSoundPlay(BGMSoundName.DungeonBoss);
        while (_light.gameObject.transform.localPosition.y > 1f) 
        {

            _light.gameObject.transform.position +=Vector3.down*0.09f;
            if (_light.intensity < 500)
            {
                _light.intensity += 5;
            }
            if (_light.range > 5)
            {
                _light.range -= 1f;
            }

            foreach (NavMeshAgent agent in pigsAgent)
            {
                agent.SetDestination(_pigs.transform.position);
            }
            yield return new WaitForSecondsRealtime(0.01f);
        }

        yield return new WaitForSecondsRealtime(1f);
        _pigs.SetActive(false);
        _aieht.gameObject.SetActive(true);

        while (_light.intensity > 200 )
        {
            _light.intensity -= 3;

            if (_light.range < 150)
            {
                _light.range += 1f;
            }
            if (_light.gameObject.transform.localPosition.y < 5f)
            {
                _light.gameObject.transform.position += Vector3.up * 0.09f;
            }
            
            if (_light.color.g>0f)
            {
                Color currentcolor = _light.color;
                _light.color = currentcolor - new Color(0, 0.1f, 0, 0f);
                yield return new WaitForSecondsRealtime(0.01f);
            }
            
        }
        _greenpigHealthSystem.OnDie -= RespawnNextBoss;
    }
}
