using UnityEngine;

public class PrepareStation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.UIManager.ShowCanvas("RestaurantUI");
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
