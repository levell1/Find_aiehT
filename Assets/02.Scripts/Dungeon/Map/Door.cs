using UnityEngine;
public class Door : MonoBehaviour
{
    public Vector3 NextRoomPosition;
    public GameObject _nextRoom;
    [SerializeField] private DungeonManager _dungeonManager ;
    private BoxCollider _boxCollider;
    private MeshRenderer _mrenderer;
    private void Awake()
    {
        _dungeonManager= FindAnyObjectByType<DungeonManager>();
        _mrenderer = GetComponent<MeshRenderer>();
        _boxCollider =GetComponent<BoxCollider>();
        _boxCollider.enabled = false;
    }
    void Start()
    {
        var dir = transform.forward * 17f;
        NextRoomPosition = transform.position + dir;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag ==TagName.Player)
        {
            _dungeonManager.GoNextRoom(NextRoomPosition);
        }
    }

    public void DoorColliderActive() 
    {
        _boxCollider.enabled=true;
        _mrenderer.enabled=false;
    }

}
