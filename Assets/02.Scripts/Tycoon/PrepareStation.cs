using UnityEngine;

public class PrepareStation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(TagName.Player))
        {
            GameManager.Instance.UIManager.ShowCanvas(UIName.RestaurantUI);
            gameObject.SetActive(false);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        GameManager.instance.UIManager.CloseLastCanvas();
    //    }
    //}
}
