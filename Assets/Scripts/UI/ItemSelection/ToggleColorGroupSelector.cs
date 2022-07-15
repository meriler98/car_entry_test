using System.Linq;

public class ToggleColorGroupSelector : ToggleGroupItemSelector<ColorData>
{
    public void SelectColorById(int id)
    {
        var foundData = Items.FirstOrDefault(x => x.Item.GetInstanceID() == id);

        if (foundData == null) return;

        _buttonDictionary[foundData].SetToggle(true, true);
    }
}