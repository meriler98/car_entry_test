using System;
using UnityEngine;

public class ToggleButtonActivatingVisual : MonoBehaviour
{
    [SerializeField] private GameObject backgroundGraphics;
    [SerializeField] private GameObject disabledGraphicsOverlay;
    [SerializeField] private ToggleButton toggleButton;

    private void Start()
    {
        toggleButton.OnToggleUpdated += Toggle_OnToggleUpdated;
        
        UpdateVisual();
    }
    
    private void OnDestroy()
    {
        toggleButton.OnToggleUpdated -= Toggle_OnToggleUpdated;
    }

    private void UpdateVisual()
    {
        disabledGraphicsOverlay.SetActive(!toggleButton.IsEnabled);
        
        backgroundGraphics.SetActive(toggleButton.IsToggleOn);
    }
    
    private void Toggle_OnToggleUpdated(object sender, EventArgs e)
    {
        UpdateVisual();
    }
}