using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfiguratorUI : MonoBehaviour
{
    [SerializeField] private PresetSelectionUI presetSelectionUI;
    [SerializeField] private ConfigurationEditorUI configuratorEditor;

    private int _selectedPresetIndex = -1;
    private PersistentData _persistentData;

    private void Start()
    {
        _persistentData = GlobalObjects.GetPersistentData();
        
        presetSelectionUI.OnLoadPreset += PresetLoader_OnLoadPreset;
        presetSelectionUI.OnCreateConfiguration += PresetLoader_OnCreateConfiguration;
        
        presetSelectionUI.Show();
        configuratorEditor.Hide();
    }

    private void OnDestroy()
    {
        presetSelectionUI.OnLoadPreset -= PresetLoader_OnLoadPreset;
        presetSelectionUI.OnCreateConfiguration -= PresetLoader_OnCreateConfiguration;
    }

    private void ChangeToConfiguratorEditor(CarConfigurationModel carModel)
    {
        configuratorEditor.SetConfigurationModel(carModel);
        presetSelectionUI.Hide();
        configuratorEditor.Show();
    }

    private void PresetLoader_OnCreateConfiguration(object sender, EventArgs e)
    {
        ChangeToConfiguratorEditor(new CarConfigurationModel());
    }

    private void PresetLoader_OnLoadPreset(object sender, PresetSelectionUI.OnLoadPresetEventArgs e)
    {
        _selectedPresetIndex = e.PresetIndex;
        ChangeToConfiguratorEditor(_persistentData.GetCarConfiguration(_selectedPresetIndex));
    }
}
