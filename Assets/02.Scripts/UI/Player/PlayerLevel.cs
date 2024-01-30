using TMPro;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private PlayerExpSystem _playerExpSystem;
    private TMP_Text _levelText;
    private void Awake()
    {
        _levelText = GetComponent<TMP_Text>();
        if (_playerExpSystem == null)
        {
            _playerExpSystem = GameObject.FindWithTag(TagName.Player).GetComponent<PlayerExpSystem>();
        }
        _playerExpSystem.OnLevelUp += Levelup;
    }

    private void Levelup(int level) 
    {
        _levelText.text = level.ToString();
    }

}
