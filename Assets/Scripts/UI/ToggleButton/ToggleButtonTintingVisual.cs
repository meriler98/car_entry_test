using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleButton))]
public class ToggleButtonTintingVisual : MonoBehaviour
{
    [SerializeField] private Image backgroundTint;
    [SerializeField] private Color toggledOffTint = Color.white;
    [SerializeField] private Color pressedOffTint = Color.white;
    [SerializeField] private Color toggledOnTint = Color.white;
    [SerializeField] private Color pressedOnTint = Color.white;
    [SerializeField] private Color disabledTint = Color.white;

    private ToggleButton _toggleButton;

    private void Start()
    {
        _toggleButton = GetComponent<ToggleButton>();
        
        _toggleButton.OnToggleUpdated += Toggle_OnToggleUpdated;
        _toggleButton.OnMouseDown += Toggle_OnMouseDown;
        
        UpdateVisual();
    }

    private void OnDestroy()
    {
        _toggleButton.OnToggleUpdated -= Toggle_OnToggleUpdated;
        _toggleButton.OnMouseDown -= Toggle_OnMouseDown;
    }

    private void UpdateVisual()
    {
        if (!_toggleButton.IsEnabled)
        {
            backgroundTint.color = disabledTint;
            return;
        }

        backgroundTint.color = _toggleButton.IsToggleOn ? toggledOnTint : toggledOffTint;
    }

    public void Toggle_OnMouseDown(object sender, EventArgs e)
    {
        backgroundTint.color = _toggleButton.IsToggleOn ? pressedOnTint : pressedOffTint;
    }

    private void Toggle_OnToggleUpdated(object sender, EventArgs e)
    {
        UpdateVisual();
    }
}