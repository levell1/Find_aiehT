using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
public class CommaText : MonoBehaviour
{
    [SerializeField] protected TMP_Text _valueText;

    private int _value;

    private void Awake()
    {
        _valueText = GetComponent<TMP_Text>();
        
    }
    private void Start()
    {
        _valueText.text = GetComma(_value).ToString();
    }
    protected virtual void ChangeGold(int Value)
    {
        _valueText.text = GetComma(Value).ToString();
    }
    private string GetComma(int data)
    {
        return string.Format("{0:#,##0}", data); 
    }
    
}
