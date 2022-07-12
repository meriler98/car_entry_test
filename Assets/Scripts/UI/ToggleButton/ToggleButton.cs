using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleButton : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    [SerializeField] private bool autoDeselectOnDisabled;
    
    private bool _isToggledOn = false;
    private bool _isEnabled = true;

    public bool IsToggleOn => _isToggledOn;
    public bool IsEnabled {
        get => _isEnabled;
        set
        {
            _isEnabled = value;
            
            if(autoDeselectOnDisabled)
                SetToggle(false, true);
            
            OnEnableChanged?.Invoke(this, new OnEnabledChangedEventArgs(value));
            OnToggleUpdated?.Invoke(this, EventArgs.Empty);
        } 
    }

    public EventHandler<OnToggledEventArgs> OnToggledEvent;
    public EventHandler<OnEnabledChangedEventArgs> OnEnableChanged;
    public EventHandler OnMouseDown;
    public EventHandler OnToggleUpdated;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!IsEnabled) return;
        
        OnMouseDown?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!IsEnabled) return;

        SetToggle(!_isToggledOn, true);
    }

    public void SetToggle(bool isToggledOn, bool notifyToggleEvent = false)
    {
        if(!IsEnabled) return;

        _isToggledOn = isToggledOn;
        
        if(notifyToggleEvent)
            OnToggledEvent?.Invoke(this, new OnToggledEventArgs(_isToggledOn));
        
        OnToggleUpdated?.Invoke(this, EventArgs.Empty);
    }

    public class OnToggledEventArgs : EventArgs
    {
        public bool IsToggledOn { get; }

        public OnToggledEventArgs(bool isToggledOn)
        {
            IsToggledOn = isToggledOn;
        }
    }
    
    public class OnEnabledChangedEventArgs : EventArgs
    {
        public bool Enabled { get; }

        public OnEnabledChangedEventArgs(bool enabled)
        {
            Enabled = enabled;
        }
    }
}