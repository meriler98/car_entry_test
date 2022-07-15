using System.Linq;

public class ToggleEngineGroupSelector : ToggleGroupItemSelector<EngineData>
{
    public void SelectEngineById(int id)
    {
        var foundData = Items.FirstOrDefault(x => x.Item.GetInstanceID() == id);

        if (foundData == null) return;

        _buttonDictionary[foundData].SetToggle(true, true);
    }
}