using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class ToggleVersionGroupSelector : ToggleGroupItemSelector<VersionData>
{
    [SerializeField] private TMP_Text descriptionText;

    private RectTransform _descriptionRect;

    protected override void Awake()
    {
        base.Awake();

        _descriptionRect = descriptionText.GetComponent<RectTransform>();
        
        OnSelectedItemsChange += VersionGroup_OnSelectedItemsChange;
    }

    public void SelectVersionById(string guid)
    {
        var foundData = Items.FirstOrDefault(x => x.Item.Guid == guid);

        if (foundData == null) return;

        _buttonDictionary[foundData].SetToggle(true, true);
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