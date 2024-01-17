using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public interface IInteractable
{
    //상호작용 할 때 상황
    string GetInteractPrompt();

    void OnInteract();
}

public class TestInter : MonoBehaviour
{
    private IInteractable curInteractable;
    private GameObject curInteractGameobject;
    public TextMeshProUGUI itemPromptText;

    private void Update()
    {
        //물체 감지
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out RaycastHit hit, 2f);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("InteractObject"))
        {
            if (hit.collider.gameObject != curInteractGameobject)
            {
                curInteractGameobject = hit.collider.gameObject;
                curInteractable = hit.collider.GetComponent<IInteractable>();
                SetPromptText();
            }
        }
        else
        {
            //초기화 및 UI 꺼주기
            curInteractGameobject = null;
            curInteractable = null;
            itemPromptText.gameObject.SetActive(false);
        }
    }

    private void SetPromptText()
    {
        itemPromptText.gameObject.SetActive(true);
        itemPromptText.text = string.Format("<b>[G]</b> {0}", curInteractable.GetInteractPrompt());
    }

    //press G key
    public void OnInteraction(InputAction.CallbackContext context)
    {
        Debug.Log("aaaaaa");
        if (curInteractable != null)
        {
            itemPromptText.gameObject.SetActive(false);
            curInteractable.OnInteract();
            curInteractGameobject = null;
            curInteractable = null;
        }
    }

}
