using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinimapSetting : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField] private Camera _minimapCamera;

    [SerializeField]
    private float _zoomMin = 1;
    [SerializeField]
    private float _zoomMax = 30;

    [SerializeField]
    private TMP_Text _mapName;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
        ChangeMapName("2");
    }

    private void Update()
    {
        transform.position = new Vector3(_player.position.x, 10, _player.position.z);

    }

    public void ChangeMapName(string mapname) 
    {
        //FIX
        //소리변경시, 맵 트리거 시 바뀌게 수정
        _mapName.text = mapname;
    }

    public void ZoomIn() 
    {
        _minimapCamera.orthographicSize = Mathf.Max(_minimapCamera.orthographicSize - 1, _zoomMin);
    }
    public void ZoomOut()
    {
        _minimapCamera.orthographicSize = Mathf.Min(_minimapCamera.orthographicSize + 1, _zoomMax);
    }
}
