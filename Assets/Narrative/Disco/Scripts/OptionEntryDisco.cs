using TMPro;
using UnityEngine;

public class OptionEntryDisco : MonoBehaviour
{
    private TextMeshProUGUI _optionText;
    private Color _defaultColor;
    private Color _selectedColor;
    void Awake()
    {
        _optionText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetUp(int index, string text, Color defaultColor, Color selectedColor)
    {
        _optionText.text = index.ToString() + ". " + text;
        _optionText.color = defaultColor;
        _defaultColor = defaultColor;
        _selectedColor = selectedColor;
    }

    public void SetColor(bool selected)
    {
        _optionText.color = selected ? _selectedColor : _defaultColor;
    }
}
