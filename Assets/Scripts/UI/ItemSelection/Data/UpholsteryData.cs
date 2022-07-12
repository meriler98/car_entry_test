using UnityEngine;
using UnityEngine.UI;

public class UpholsteryData : ItemData<UpholsteryInfoSO>
{
    [SerializeField] private Image upholsteryColor;
    
    public override void UpdateDraw()
    {
        if (Item != null && upholsteryColor != null)
            upholsteryColor.color = Item.Color;
    }
}