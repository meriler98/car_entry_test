using UnityEngine;

public class ConfigurationEditorUI : MonoBehaviour
{
    [SerializeField] private ConfiguratorDataSO configurationData;
    [SerializeField] private VersionSelectionView versionSelectionView;
    
    private CarConfigurationModel _currentModel;
    private int _currentEditingIndex = -1;

    public void Show()
    {
        gameObject.SetActive(true);
        
        versionSelectionView.OnItemSelected += VersionSelection_OnVersionInfoSelected;
        versionSelectionView.SetItems(configurationData.VersionInfos);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        
        versionSelectionView.OnItemSelected -= VersionSelection_OnVersionInfoSelected;
    }
    
    public void SetConfigurationModel(CarConfigurationModel carConfigurationModel)
    {
        _currentModel = carConfigurationModel;
    }

    public void SetConfigurationEditingIndex(int index)
    {
        _currentEditingIndex = index;
    }
    
    private void VersionSelection_OnVersionInfoSelected(object sender, VersionSelectionView.OnItemSelectedEventArgs<VersionInfoSO> e)
    {
        if(_currentModel == null) return;
        
        _currentModel.SetVersion(e.ItemSelected);
    }
}