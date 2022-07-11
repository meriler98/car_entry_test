using UnityEngine;

public class ConfigurationEditorUI : MonoBehaviour
{
    [SerializeField] private ConfiguratorDataSO configurationData;
    [SerializeField] private VersionSelectionView versionSelectionView;
    [SerializeField] private EngineSelectionView engineSelectionView;
    
    private CarConfigurationModel _currentModel;
    private int _currentEditingIndex = -1;

    public void Show()
    {
        gameObject.SetActive(true);
        
        InitializeViews();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        
        DeinitializeView();
    }

    private void InitializeViews()
    {
        versionSelectionView.OnItemSelected += VersionSelection_OnVersionInfoSelected;
        versionSelectionView.SetItems(configurationData.VersionInfos);
        
        engineSelectionView.OnItemSelected += EngineSelection_OnItemSelected;
        engineSelectionView.SetItems(configurationData.EngineInfos);
    }

    private void DeinitializeView()
    {
        versionSelectionView.OnItemSelected -= VersionSelection_OnVersionInfoSelected;
        engineSelectionView.OnItemSelected -= EngineSelection_OnItemSelected;
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

    private void EngineSelection_OnItemSelected(object sender, ItemSelectionView<EngineInfoSO>.OnItemSelectedEventArgs<EngineInfoSO> e)
    {
        if(_currentModel == null) return;
        
        _currentModel.SetEngine(e.ItemSelected);
    }
}