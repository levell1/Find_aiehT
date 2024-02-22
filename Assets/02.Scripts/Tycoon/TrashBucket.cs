using UnityEngine;

public class TrashBucket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.Player))
        {
            GameManager.Instance.Player.GetComponent<ServingFood>().CanThrowAway = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag(TagName.Player))
        {
            GameManager.Instance.Player.GetComponent<ServingFood>().CanThrowAway = false;
        }
    }
}
