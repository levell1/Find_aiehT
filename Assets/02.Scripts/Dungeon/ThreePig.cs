using System.Collections;
using UnityEngine;
public class ThreePig : MonoBehaviour
{
    [SerializeField] private BossHealthSystem _greenPigHealthSystem;
    [SerializeField] private Transform[] _pigsTransform = new Transform[3];
    [SerializeField] private Vector3[] _pigsPosition = new Vector3[3];

    private void Awake()
    {
        _greenPigHealthSystem.OnDie += ThreePigOn;
    }

    private void Start()
    {
        for (int i = 0; i < _pigsTransform.Length; i++)
        {
            _pigsTransform[i].gameObject.SetActive(false);
        }
    }

    private void ThreePigOn()
    {
        StartCoroutine(ThreePigPattern());
    }

    private IEnumerator ThreePigPattern() 
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);

            for (int i = 0; i < _pigsTransform.Length; i++)
            {
                _pigsPosition[i] = _pigsTransform[i].transform.position;
                _pigsTransform[i].gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(6f);

            for (int i = 0; i < _pigsTransform.Length; i++)
            {
                _pigsTransform[i].transform.position = _pigsPosition[i];
                _pigsTransform[i].gameObject.SetActive(false);
            }
        }
    }
}
