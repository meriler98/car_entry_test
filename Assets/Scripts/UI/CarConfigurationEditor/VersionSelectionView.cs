using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VersionSelectionView : ItemSelectionView<VersionInfoSO>
{
    [SerializeField] private ToggleTextButtonUI textToggle;
    [SerializeField] private TMP_Text descriptionText;

    protected override ToggleButtonUI AddToggle(VersionInfoSO item)
    {
        var toggle = toggleGroup.AddToggleToGroup(textToggle) as ToggleTextButtonUI;
        
        toggle.SetText(item.VersionName);

        return toggle;
    }

    protected override void ItemSelected(ToggleButtonUI toggle, bool IsToggledOn, VersionInfoSO correspondingItem)
    {
        if (IsToggledOn)
        {
            descriptionText.text = correspondingItem.Description;
        }
        else
        {
            descriptionText.text = "";
        }
    }
}


