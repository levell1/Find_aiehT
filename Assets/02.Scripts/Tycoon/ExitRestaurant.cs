using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRestaurant : MonoBehaviour
{
    [SerializeField] private GameObject _exitText;

    ServingFood _servingFood;
    private void Start()
    {
        _servingFood = GameManager.Instance.Player.GetComponent<ServingFood>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.Player))
        {
            _exitText.SetActive(true);
            _servingFood.CanExitRestaurant = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.Player))
        {
            _exitText.SetActive(false);
            _servingFood.CanExitRestaurant = false;
        }
    }
}
