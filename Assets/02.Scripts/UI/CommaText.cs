using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
public class CommaText : MonoBehaviour
{
    [SerializeField] protected TMP_Text _valueText;

    public int TextValue;

    private void Awake()
    {
        _valueText = GetComponent<TMP_Text>();
        
    }

    public void ChangeGold(int Value)
    {
        _valueText.text = GetComma(Value).ToString();
    }
    private string GetComma(int data)
    {
        return string.Format("{0:#,##0}", data); 
    }
    
}
