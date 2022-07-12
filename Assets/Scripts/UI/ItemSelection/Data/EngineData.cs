using TMPro;
using UnityEngine;

public class EngineData : ItemData<EngineInfoSO>
{
    [SerializeField] private TMP_Text engineText;
        
    public override void UpdateDraw()
    {
        if (engineText != null && Item != null)
            engineText.text = Item.EngineName;
    }
}