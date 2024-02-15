using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Vector3 NextRoomPosition;
    [SerializeField] private GameObject _nextRoom;
    [SerializeField] private DungeonManager _dunheonManager ;
    private BoxCollider BoxCollider;
    private MeshRenderer Renderer;
    private void Awake()
    {
        _dunheonManager= FindAnyObjectByType<DungeonManager>();
        Renderer = GetComponent<MeshRenderer>();
        BoxCollider =GetComponent<BoxCollider>();
        BoxCollider.enabled = false;
    }
    void Start()
    {
        var dir = transform.forward * 15f;
        NextRoomPosition = transform.position + dir + Vector3.up;
        
    }
    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        _dunheonManager.GoNextRoom(NextRoomPosition);
    }

    public void DoorColliderActive() 
    {
        BoxCollider.enabled=true;
        Renderer.enabled=false;
    }

}
