using TMPro;
using UnityEngine;

public class AdditionalPackageData : ItemData<AdditionalPackageInfoSO>
{
    [SerializeField] private TMP_Text additionalPackageText;
    
    public override void UpdateDraw()
    {
        if (Item != null && additionalPackageText != null)
            additionalPackageText.text = Item.PackageName;
    }
}