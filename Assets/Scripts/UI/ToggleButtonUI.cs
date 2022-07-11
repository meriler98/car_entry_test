using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image backgroundGraphics;
    [SerializeField] private Color toggleOffTint;
    [SerializeField] private Color toggleOnTint;
    [SerializeField] private Color pressedTint;

    private bool _isToggledOn = false;

    public bool IsToggleOn => _isToggledOn;

    public EventHandler<OnToggledEventArgs> OnToggled;

    public void OnPointerDown(PointerEventData eventData)
    {
        backgroundGraphics.color = pressedTint;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetToggle(!_isToggledOn, true);
    }

    public void SetToggle(bool isToggledOn, bool notifyToggleEvent = false)
    {
        _isToggledOn = isToggledOn;
        UpdateToggleVisual();
        
        if(notifyToggleEvent)
            OnToggled?.Invoke(this, new OnToggledEventArgs(GetInstanceID(), _isToggledOn));
    }

    private void UpdateToggleVisual()
    {
        backgroundGraphics.color = _isToggledOn ? toggleOnTint : toggleOffTint;
    }

    public class OnToggledEventArgs : EventArgs
    {
        public int InstanceID { get; }
        public bool IsToggledOn { get; }

        public OnToggledEventArgs(int instanceID, bool isToggledOn)
        {
            InstanceID = instanceID;
            IsToggledOn = isToggledOn;
        }
    }
}