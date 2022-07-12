using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToggleButtonGroup : MonoBehaviour
{
    [SerializeField] private bool isSingleSelection;
    [SerializeField] private ToggleButton[] toggleButtons;

    private List<ToggleButton> _selectedButtons = new List<ToggleButton>();
    private bool _ignoreNotify = false; // To prevent stack overflow from recalling here after changing toggle state

    public ToggleButton[] SelectedButtons => _selectedButtons.ToArray();
    public ToggleButton[] ToggleButtons => toggleButtons.ToArray();

    public EventHandler OnSelectionChanged;

    private void Start()
    {
        foreach (var button in toggleButtons)
        {
            button.OnToggledEvent += Button_OnToggled;
        }
    }

    public void SetSelection(params ToggleButton[] selection)
    {
        _selectedButtons.Clear();
        
        foreach (var button in toggleButtons)
        {
            button.SetToggle(selection.Contains(button), true);

            _selectedButtons.Add(button);

            // After first run if we are on single selection then ignore everything else
            if (isSingleSelection) break;
        }
        
        OnSelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ClearSelection()
    {
        _selectedButtons.Clear();
        
        foreach (var button in toggleButtons)
        {
            button.SetToggle(false, true);
        }
        
        OnSelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddSelection(ToggleButton toggleButton)
    {
        if(_selectedButtons.Contains(toggleButton)) return;

        if (isSingleSelection)
            ClearSelection();

        toggleButton.SetToggle(true, true);
        _selectedButtons.Add(toggleButton);
        
        OnSelectionChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void RemoveSelection(ToggleButton toggleButton)
    {
        if(!_selectedButtons.Contains(toggleButton)) return;
        
        toggleButton.SetToggle(false, true);
        _selectedButtons.Remove(toggleButton);
        
        OnSelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Button_OnToggled(object sender, ToggleButton.OnToggledEventArgs e)
    {
        if(_ignoreNotify) return;
        
        var toggleButton = sender as ToggleButton;
        
        // Clearing other items
        if(e.IsToggledOn && isSingleSelection)
            _selectedButtons.Clear();
        
        _ignoreNotify = true;
        
        if (e.IsToggledOn)
            AddSelection(toggleButton);
        else
            RemoveSelection(toggleButton);

        _ignoreNotify = false;
        
        // Updating item on the list
        
        
        OnSelectionChanged?.Invoke(this, EventArgs.Empty);
    }
}