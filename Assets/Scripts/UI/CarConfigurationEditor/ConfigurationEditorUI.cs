using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationEditorUI : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button backButton;
    
    private ToggleVersionGroupSelector _versionSelector;
    private ToggleEngineGroupSelector _engineSelector;
    private ToggleColorGroupSelector _colorSelector;
    private ToggleUpholsteryGroupSelector _upholsterySelector;
    private ToggleAdditionalPackageGroupSelector _additionalPackageSelector;

    private CarConfigurationModel _currentModel;
    private int _currentEditingIndex = -1;

    public EventHandler OnBackPressed;
    public EventHandler<OnSavePressedEventArgs> OnSaved;

    private void Start()
    {
        saveButton.onClick.AddListener(SaveButton_OnSave);
        backButton.onClick.AddListener(BackButton_OnBack);
        
        LoadItems();
    }

    private void OnDestroy()
    {
        saveButton.onClick.RemoveListener(SaveButton_OnSave);
        backButton.onClick.RemoveListener(BackButton_OnBack);
    }

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

    private void LoadItems()
    {
        _versionSelector = GetComponentInChildren<ToggleVersionGroupSelector>();
        _engineSelector = GetComponentInChildren<ToggleEngineGroupSelector>();
        _colorSelector = GetComponentInChildren<ToggleColorGroupSelector>();
        _upholsterySelector = GetComponentInChildren<ToggleUpholsteryGroupSelector>();
        _additionalPackageSelector = GetComponentInChildren<ToggleAdditionalPackageGroupSelector>();
    }

    private void InitializeViews()
    {
        if(_currentModel != null)
        {
            if(_currentModel.VersionInfoId != 0)
                _versionSelector.SelectVersionById(_currentModel.VersionInfoId);
            if (_currentModel.EngineInfoId != 0)
                _engineSelector.SelectEngineById(_currentModel.EngineInfoId);
            if(_currentModel.ColorInfoId != 0)
                _colorSelector.SelectColorById(_currentModel.ColorInfoId);
            if(_currentModel.UpholsteryInfoId != 0)
                _upholsterySelector.SelectUpholsteryById(_currentModel.UpholsteryInfoId);


            if (_currentModel.AdditionalPackageInfoIds != null)
            {
                foreach (var packageId in _currentModel.AdditionalPackageInfoIds)
                {
                    _additionalPackageSelector.SelectAdditionalPackageById(packageId);
                }
            }
        }
        else
        {
            _currentModel = new CarConfigurationModel();
        }

        _versionSelector.OnSelectedItemsChange += VersionSelector_OnVersionSelected;
        _engineSelector.OnSelectedItemsChange += EngineSelector_OnEngineSelected;
        _colorSelector.OnSelectedItemsChange += ColorSelector_OnColorSelected;
        _upholsterySelector.OnSelectedItemsChange += UpholsterySelector_OnUpholsterySelected;
        _additionalPackageSelector.OnSelectedItemsChange += AdditionalPackagesSelector_OnAdditionalPackagesSelected;
    }

    private void DeinitializeView()
    {
        _versionSelector.OnSelectedItemsChange -= VersionSelector_OnVersionSelected;
        _engineSelector.OnSelectedItemsChange -= EngineSelector_OnEngineSelected;
        _colorSelector.OnSelectedItemsChange -= ColorSelector_OnColorSelected;
        _upholsterySelector.OnSelectedItemsChange -= UpholsterySelector_OnUpholsterySelected;
        _additionalPackageSelector.OnSelectedItemsChange -= AdditionalPackagesSelector_OnAdditionalPackagesSelected;
    }

    public void SetConfigurationModel(CarConfigurationModel carConfigurationModel)
    {
        _currentModel = carConfigurationModel;
    }

    public void SetConfigurationEditingIndex(int index)
    {
        _currentEditingIndex = index;
    }

    private void UpdateItemsForVersion(VersionInfoSO versionInfo)
    {
        // TODO: After version selection update the availability of some items 
    }

    #region Subscribtions

    private void VersionSelector_OnVersionSelected(object sender, EventArgs e)
    {
        if(_currentModel == null) return;

        var item = _versionSelector.SelectedItems.Length == 0 ? null : _versionSelector.SelectedItems[0].Item;
        _currentModel.SetVersion(item);
        UpdateItemsForVersion(item);
    }

    private void EngineSelector_OnEngineSelected(object sender, EventArgs e)
    {
        if (_currentModel == null) return;

        var item = _engineSelector.SelectedItems.Length == 0 ? null : _engineSelector.SelectedItems[0].Item;
        _currentModel.SetEngine(item);
    }

    private void ColorSelector_OnColorSelected(object sender, EventArgs e)
    {
        if (_currentModel == null) return;

        var item = _colorSelector.SelectedItems.Length == 0 ? null : _colorSelector.SelectedItems[0].Item;
        _currentModel.SetColor(item);
    }

    private void UpholsterySelector_OnUpholsterySelected(object sender, EventArgs e)
    {
        if (_currentModel == null) return;

        var item = _upholsterySelector.SelectedItems.Length == 0 ? null : _upholsterySelector.SelectedItems[0].Item;
        _currentModel.SetUpholstery(item);
    }

    private void AdditionalPackagesSelector_OnAdditionalPackagesSelected(object sender, EventArgs e)
    {
        if (_currentModel == null) return;

        var items = _additionalPackageSelector.SelectedItems.Length == 0 ?
            new AdditionalPackageInfoSO[0] :
            _additionalPackageSelector.SelectedItems.Select(x => x.Item).ToArray();
        _currentModel.SetAdditionalPackageInfos(items);
    }

    private void SaveButton_OnSave()
    {
        OnSaved?.Invoke(this, new OnSavePressedEventArgs(_currentModel, _currentEditingIndex));
    }
    
    private void BackButton_OnBack()
    {
        OnBackPressed?.Invoke(this, EventArgs.Empty);
    }
    
    #endregion

    public class OnSavePressedEventArgs : EventArgs
    {
        public CarConfigurationModel ModelToSave;
        public int IndexToUpdate;

        public OnSavePressedEventArgs(CarConfigurationModel modelToSave, int indexToUpdate)
        {
            ModelToSave = modelToSave;
            IndexToUpdate = indexToUpdate;
        }
    }
}