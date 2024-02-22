using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private DungeonManager _dungeonManager;

    private void Awake()
    {
        _dungeonManager = FindAnyObjectByType<DungeonManager>();
        transform.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagName.Player)) 
        {
            _dungeonManager.GoNextStage();
        }
    }

}
