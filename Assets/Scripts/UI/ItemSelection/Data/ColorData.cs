using UnityEngine;
using UnityEngine.UI;

public class ColorData : ItemData<ColorInfoSO>
{
    [SerializeField] private Image imageColor;
    [SerializeField] private GameObject asteriskOption;
    
    public override void UpdateDraw()
    {
        if(Item == null) return;
        
        if (imageColor != null)
            imageColor.color = Item.Color;
        
        if(asteriskOption != null)
            asteriskOption.SetActive(Item.IsOption);
    }
}