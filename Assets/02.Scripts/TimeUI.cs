using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private GameObject _timeui;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == SceneName.DungeonScene)
        {
            _timeui.SetActive(false);
        }
        else
        {
            _timeui.SetActive(true);
        }

    }
}
