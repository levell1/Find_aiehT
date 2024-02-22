using UnityEngine;

public class PrepareStation : MonoBehaviour
{
    [SerializeField] private GameObject _prepareText;

    ServingFood _servingFood;
    private void Start()
    {
        _servingFood = GameManager.Instance.Player.GetComponent<ServingFood>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.Player))
        {
            _prepareText.SetActive(true);
            _servingFood.CanOpenRecipeUI = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.Player))
        {
            _prepareText.SetActive(false);
            _servingFood.CanOpenRecipeUI = false;
        }
    }

    public void OffPrepareStation()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        _prepareText.SetActive(false);
        _servingFood.CanOpenRecipeUI = false;
    }
}
