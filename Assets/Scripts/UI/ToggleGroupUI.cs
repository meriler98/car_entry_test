using System;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGroupUI : MonoBehaviour
{
    [SerializeField] private bool singleSelection;
    [SerializeField] private ToggleButtonUI toggleTemplate;
    
    private List<ToggleButtonUI> _toggles = new List<ToggleButtonUI>();

    public ToggleButtonUI[] Toggles => _toggles.ToArray();
    
    public EventHandler<OnToggleSelectedEventArgs> OnToggleButtonSelected;

    public ToggleButtonUI AddToggleToGroup()
    {
        var newToggle = Instantiate(toggleTemplate, transform);
        
        _toggles.Add(newToggle);
        newToggle.OnToggled += Toggle_OnToggled;

        return newToggle;
    }

    public void RemoveToggleFromGroup(ToggleButtonUI toggle)
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

    public void SetToggleSelection(ToggleButtonUI toggle, bool isToggledOn, bool notifySelection = false)
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
        var toggle = (ToggleButtonUI) sender;
        
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