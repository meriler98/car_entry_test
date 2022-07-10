using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ItemSelectionView<T> : MonoBehaviour
{
    [SerializeField] private ToggleGroupUI toggleGroup;

    private RectTransform _cachedRectTranform;
    private T[] _item;
    private Dictionary<ToggleButtonUI, T> _itemDirctionary = new Dictionary<ToggleButtonUI, T>();

    public EventHandler<OnItemSelectedEventArgs<T>> OnItemSelected;

    private void Awake()
    {
        _cachedRectTranform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        toggleGroup.OnToggleButtonSelected += ToggleGroup_OnToggleButtonSelected;
    }

    private void OnDestroy()
    {
        toggleGroup.OnToggleButtonSelected += ToggleGroup_OnToggleButtonSelected;
    }

    public void SetItems(params T[] items)
    {
        _item = items;

        UpdateToggles();
    }

    /*public void SetToggleSelectionByVersionId(int versionId, bool isToggleOn)
    {
        var toggleForVersion = _versionInfoDirctionary
            .FirstOrDefault(x => x.Value.GetInstanceID() == versionId)
            .Key;

        if (toggleForVersion == null)
        {
            Debug.LogError("Toggle with such Version Info ID was not found");
            return;
        }

        UpdateItem(toggleForVersion, isToggleOn);
    }*/

    protected void UpdateItem(ToggleButtonUI toggle, bool isToggledOn)
    {
        var correspontingItem = _itemDirctionary[toggle];
                
        if (correspontingItem == null)
        {
            Debug.LogError("Couldn't find toggle stored in dictionary!");
        }
        
        ItemSelected(toggle, isToggledOn, correspontingItem);

        // Force rebuild UI for content fitter
        UIUtilities.ForceLayoutRebuild(this, _cachedRectTranform);
    }

    protected void UpdateToggles()
    {
        // Removing old toggles
        toggleGroup.ClearToggles();
        
        _itemDirctionary.Clear();

        // Replacing with new ones
        foreach (var item in _item)
        {
            var toggle = toggleGroup.AddToggleToGroup();
            
            UpdateToggleVisual(toggle, item);

            _itemDirctionary[toggle] = item;
        }
    }

    protected virtual void UpdateToggleVisual(ToggleButtonUI toggle, T item) { }

    protected virtual void ItemSelected(ToggleButtonUI toggle, bool IsToggledOn, T correspondingItem)
    {
        
    }

    private void ToggleGroup_OnToggleButtonSelected(object sender, ToggleGroupUI.OnToggleSelectedEventArgs e)
    {
        var toggleVersionPair = _itemDirctionary
            .FirstOrDefault(x => x.Key == e.SelectedToggle);

        if (toggleVersionPair.Value == null)
        {
            Debug.LogError("Failed to find corresponding version info");
            return;
        }
        
        UpdateItem(e.SelectedToggle, e.IsToggledOn);
        
        if(e.IsToggledOn)
            OnItemSelected?.Invoke(this, new OnItemSelectedEventArgs<T>(toggleVersionPair.Value));
    }

    public class OnItemSelectedEventArgs<T> : EventArgs
    {
        public T ItemSelected { get; }

        public OnItemSelectedEventArgs(T versionSelected)
        {
            ItemSelected = versionSelected;
        }
    }
}