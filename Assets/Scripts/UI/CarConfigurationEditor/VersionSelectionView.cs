using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VersionSelectionView : ItemSelectionView<VersionInfoSO>
{
    [SerializeField] private TMP_Text descriptionText;

    protected override void UpdateToggleVisual(ToggleButtonUI toggle, VersionInfoSO item)
    {
        toggle.SetButtonText(item.VersionName);
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


