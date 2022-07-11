using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSelectionView : ItemSelectionView<EngineInfoSO, ToggleTextButtonUI>
{
    protected override void UpdateToggleVisual(ToggleTextButtonUI toggle, EngineInfoSO item)
    {
        toggle.SetText(item.EngineName);
    }
}
