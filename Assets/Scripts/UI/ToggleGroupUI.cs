using System;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGroupUI<T> : MonoBehaviour where T : ToggleButtonUI
{
    [SerializeField] private bool singleSelection;
    [SerializeField] private T toggleTemplate;
    
    private List<T> _toggles = new List<T>();

    public T[] Toggles => _toggles.ToArray();
    
    public EventHandler<OnToggleSelectedEventArgs> OnToggleButtonSelected;

    public T AddToggleToGroup()
    {
        var newToggle = Instantiate(toggleTemplate, transform);
        
        _toggles.Add(newToggle);
        newToggle.OnToggled += Toggle_OnToggled;

        return newToggle;
    }

    public void RemoveToggleFromGroup(T toggle)
    {
        if(!_toggles.Contains(toggle)) return;

        _toggles.Remove(toggle);
        toggle.OnToggled -= Toggle_OnToggled;
    }

    public void ClearToggles()
    {
        foreach (var toggle in _toggles)
        {
            toggle.OnToggled -= Toggle_OnToggled;
            Destroy(toggle.gameObject);
        }
        
        _toggles.Clear();
    }

    public void SetToggleSelection(T toggle, bool isToggledOn, bool notifySelection = false)
    {
        if (!_toggles.Contains(toggle))
        {
            Debug.LogError("Toggle does not exists in current toggle group");
            return;
        }

        if (singleSelection)
        {
            foreach (var currentToggle in _toggles)
            {
                if(currentToggle == toggle) continue;
                
                currentToggle.SetToggle(false);
            }
        }

        toggle.SetToggle(isToggledOn, notifySelection);
    }

    public void SelectToggleByIndex(int index, bool isToggledOn)
    {
        _toggles[index].SetToggle(isToggledOn);
    }

    public void ClearToggleSelection()
    {
        foreach (var toggle in _toggles)
        {
            toggle.SetToggle(false);
        }
    }

    private void Toggle_OnToggled(object sender, ToggleButtonUI.OnToggledEventArgs e)
    {
        var toggle = (T) sender;
        
        SetToggleSelection(toggle, e.IsToggledOn);
        
        OnToggleButtonSelected?.Invoke(this, new OnToggleSelectedEventArgs(toggle, e.IsToggledOn));
    }

    public class OnToggleSelectedEventArgs : EventArgs
    {
        public ToggleButtonUI SelectedToggle { get; }
        public bool IsToggledOn { get; }

        public OnToggleSelectedEventArgs(ToggleButtonUI selectedToggle, bool isToggledOn)
        {
            SelectedToggle = selectedToggle;
            IsToggledOn = isToggledOn;
        }
    }
}