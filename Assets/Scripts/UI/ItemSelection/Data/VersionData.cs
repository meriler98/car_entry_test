using TMPro;
using UnityEngine;

public class VersionData : ItemData<VersionInfoSO>
{
    [SerializeField] private TMP_Text toggleText;
    
    public override void UpdateDraw()
    {
        if(toggleText != null && Item != null)
            toggleText.text = Item.VersionName;
    }
}