using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VersionSelectionView : ItemSelectionView<VersionInfoSO, ToggleTextButtonUI>
{
    [SerializeField] private TMP_Text descriptionText;

    protected override void UpdateToggleVisual(ToggleTextButtonUI toggle, VersionInfoSO item)
    {
        toggle.SetText(item.VersionName);
    }

    protected override void ItemSelected(ToggleTextButtonUI toggle, bool IsToggledOn, VersionInfoSO correspondingItem)
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


