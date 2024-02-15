using TMPro;
using UnityEngine;
public class CommaText : MonoBehaviour
{
    [SerializeField] protected TMP_Text _valueText;

    protected int _Value;

    private void Awake()
    {
        _valueText = GetComponent<TMP_Text>();
    }
    protected virtual void Update()
    {
        _valueText.text = GetComma(_Value).ToString();
    }
    private string GetComma(int data)
    {
        return string.Format("{0:#,##0}", data); 
    }
    
}
