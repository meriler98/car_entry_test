using TMPro;
using UnityEngine;

public class ToggleTextButtonUI : ToggleButtonUI
{
    [SerializeField] private TMP_Text textField;

    public void SetText(string text)
    {
        this.textField.text = text;
    }
}