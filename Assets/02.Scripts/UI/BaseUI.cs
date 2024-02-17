using System.Collections;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    protected void CloseUI()
    {
        GameManager.Instance.UIManager.CloseLastCanvas();
    }

    protected IEnumerator ShowPopupForSeconds(GameObject UI,float sec)
    {
        UI.SetActive(true);
        yield return new WaitForSecondsRealtime(sec);
        UI.SetActive(false);
    }

    // 공통 기능 추가 생각
}
