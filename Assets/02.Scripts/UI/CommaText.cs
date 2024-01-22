using TMPro;
using UnityEngine;

public class CommaText : MonoBehaviour
{
    [SerializeField] protected TMP_Text _glodText;

    protected int _Value;

    private void Awake()
    {
        _glodText = GetComponent<TMP_Text>();
    }
    protected void Update()
    {
        _glodText.text = GetComma(_Value).ToString();
    }
    public string GetComma(int data)
    {
        return string.Format("{0:#,###}", data); 
    }
    
}
