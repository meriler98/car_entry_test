using System.Linq;

public class ToggleAdditionalPackageGroupSelector : ToggleGroupItemSelector<AdditionalPackageData>
{
    public void SelectAdditionalPackageById(int id)
    {
        var foundData = Items.FirstOrDefault(x => x.Item.GetInstanceID() == id);

        if (foundData == null) return;

        _buttonDictionary[foundData].SetToggle(true, true);
    }
}