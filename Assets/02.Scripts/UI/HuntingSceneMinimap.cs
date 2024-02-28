using UnityEngine;

public class HuntingSceneMinimap : MonoBehaviour
{
    [SerializeField] private GameObject _minimapObject;
    [SerializeField] private GameObject _camera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (_minimapObject.activeSelf)
            {
                _camera.SetActive(false);
                _minimapObject.SetActive(false);
            }
            else
            {
                _camera.SetActive(true);
                _minimapObject.SetActive(true);
            }
        }
    }
}
