using System;
using TMPro;
using UnityEngine;

public class ToggleVersionGroupSelector : ToggleGroupItemSelector<VersionData>
{
    [SerializeField] private TMP_Text descriptionText;

    private RectTransform _descriptionRect;

    private void Awake()
    {
        _descriptionRect = descriptionText.GetComponent<RectTransform>();
        
        OnSelectedItemsChange += VersionGroup_OnSelectedItemsChange;
    }

    private void VersionGroup_OnSelectedItemsChange(object sender, EventArgs e)
    {
        if (SelectedItems.Length == 0)
            descriptionText.text = "";
        else
            descriptionText.text = SelectedItems[0].Item.Description;
        
        UIUtilities.ForceLayoutRebuild(this, _descriptionRect);
    }
}