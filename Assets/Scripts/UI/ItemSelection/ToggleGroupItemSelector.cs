using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ToggleGroupItemSelector<T> : MonoBehaviour
{
    [SerializeField] private ToggleButtonGroup toggleButtonGroup;

    private T[] _items;
    private List<T> _selectedItems = new List<T>();
    private Dictionary<T, ToggleButton> _buttonDictionary = new Dictionary<T, ToggleButton>();

    public T[] Items => _items;
    public T[] SelectedItems => _selectedItems.ToArray();

    public EventHandler OnSelectedItemsChange;

    private void Start()
    {
        Initialize();
        
        toggleButtonGroup.OnSelectionChanged += ToggleGroup_SelectionChanged;
    }

    private void OnDestroy()
    {
        toggleButtonGroup.OnSelectionChanged -= ToggleGroup_SelectionChanged;
    }

    private void Initialize()
    {
        foreach (var button in toggleButtonGroup.ToggleButtons)
        {
            var item = button.GetComponent<T>();
            
            if(item == null) break;

            _buttonDictionary[item] = button;
        }
        
        _items = _buttonDictionary.Keys.ToArray();
    }

    public void AddSelection(T item)
    {
        toggleButtonGroup.AddSelection(_buttonDictionary[item]);
    }

    public void RemoveSelection(T item)
    {
        toggleButtonGroup.RemoveSelection(_buttonDictionary[item]);
    }

    public void SetEnableSelection(T item, bool enable)
    {
        var button = toggleButtonGroup.ToggleButtons.FirstOrDefault(x => _buttonDictionary[item] == x);
        
        if(button == null) return;

        button.IsEnabled = enable;
    }

    public void ToggleGroup_SelectionChanged(object sender, EventArgs e)
    {
        _selectedItems.Clear();
        
        foreach (var toggleButton in toggleButtonGroup.SelectedButtons)
        {
            T item = toggleButton.GetComponent<T>();
            
            if(item == null) continue;
            
            _selectedItems.Add(item);
        }
        
        OnSelectedItemsChange?.Invoke(this, EventArgs.Empty);
    }
}