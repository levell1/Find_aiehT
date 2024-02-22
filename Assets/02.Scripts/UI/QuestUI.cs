using UnityEngine;
using UnityEngine.UI;

public class QuestUI : BaseUI
{
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _exitButton.onClick.AddListener(base.CloseUI);
    }
}
