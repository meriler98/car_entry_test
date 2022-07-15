using System.Linq;

public class ToggleUpholsteryGroupSelector : ToggleGroupItemSelector<UpholsteryData>
{
    public void SelectUpholsteryById(int id)
    {
        var foundData = Items.FirstOrDefault(x => x.Item.GetInstanceID() == id);

        if (foundData == null) return;

        _buttonDictionary[foundData].SetToggle(true, true);
    }
}