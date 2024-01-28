using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareStation : MonoBehaviour
{
    [SerializeField] GameObject _restaurantUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other == _restaurantUI)
        {
            _restaurantUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == _restaurantUI)
        {
            _restaurantUI.SetActive(false);
        }
    }
}
