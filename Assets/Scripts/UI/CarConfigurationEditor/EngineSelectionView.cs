using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSelectionView : ItemSelectionView<EngineInfoSO>
{
    [SerializeField] private ToggleTextButtonUI textToggle;
    
    protected override ToggleButtonUI AddToggle(EngineInfoSO item)
    {
        var toggle = toggleGroup.AddToggleToGroup(textToggle) as ToggleTextButtonUI;
        
        toggle.SetText(item.EngineName);

        return toggle;
    }
}
