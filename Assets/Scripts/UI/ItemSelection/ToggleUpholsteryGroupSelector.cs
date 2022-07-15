using System.Linq;

public class ToggleUpholsteryGroupSelector : ToggleGroupItemSelector<UpholsteryData>
{
    public void SelectUpholsteryById(int id)
    {
        var foundData = Items.FirstOrDefault(x => x.Item.GetInstanceID() == id);

        if (foundData == null) return;

        _buttonDictionary[foundData].SetToggle(true, true);
    }

    public void EnableButtonsByVersionCompatibility(int versionId)
    {
        var foundVersion = GlobalObjects.GetPersistentData().ConfigurationLookup.VersionInfos
            .First(x => x.GetInstanceID() == versionId);

        foreach (var buttonPair in _buttonDictionary)
        {
            if (foundVersion == null)
            {
                buttonPair.Value.IsEnabled = false;
                continue;
            }

            buttonPair.Value.IsEnabled = buttonPair.Key.Item.CompatibleVersions.Contains(foundVersion);
        }
    }
}