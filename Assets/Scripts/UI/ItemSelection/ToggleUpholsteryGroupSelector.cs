using System.Linq;

public class ToggleUpholsteryGroupSelector : ToggleGroupItemSelector<UpholsteryData>
{
    public void SelectUpholsteryById(string guid)
    {
        var foundData = Items.FirstOrDefault(x => x.Item.Guid == guid);

        if (foundData == null) return;

        _buttonDictionary[foundData].SetToggle(true, true);
    }

    public void EnableButtonsByVersionCompatibility(string versionGuid)
    {
        var foundVersion = GlobalObjects.GetPersistentData().ConfigurationLookup.VersionInfos
            .First(x => x.Guid == versionGuid);

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