using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObj : MonoBehaviour, IInteractable
{
    public TestObjSO item;

    public string GetInteractPrompt()
    {
        return string.Format("Pickup {0}", item.ItemName);
    }

    public void OnInteract()
    {
        gameObject.SetActive(false);
    }
}
