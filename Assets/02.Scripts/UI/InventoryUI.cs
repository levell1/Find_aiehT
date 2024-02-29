using System.Collections;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory Inventory;

    private bool _isActiveUI;

    private Coroutine _coroutine;

    private void Awake()
    {
        Inventory = GameManager.Instance.Inventory;
        _isActiveUI = false;
    }

    private void OnEnable()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(ActiveUIC0());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && _isActiveUI)
        {
            Inventory.InActiveUI();
        }
    }

    private void OnDisable()
    {
        _isActiveUI = false;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator ActiveUIC0()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        _isActiveUI = true;
        _coroutine = null;
    }
}
