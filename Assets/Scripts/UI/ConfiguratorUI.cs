using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfiguratorUI : MonoBehaviour
{
    [SerializeField] private PresetSelectionUI presetSelectionUI;
    [SerializeField] private ConfigurationEditorUI configuratorEditor;
    [SerializeField] private SavePresetDialog savePresetDialog;


    private int _selectedPresetIndex = -1;
    private PersistentData _persistentData;

    private void Start()
    {
        _persistentData = GlobalObjects.GetPersistentData();
        
        presetSelectionUI.OnLoadPreset += PresetLoader_OnLoadPreset;
        presetSelectionUI.OnCreateConfiguration += PresetLoader_OnCreateConfiguration;
        configuratorEditor.OnBackPressed += ConfigurationEditor_OnBackPressed;
        configuratorEditor.OnSaved += ConfigurationEditor_OnSaved;
        
        presetSelectionUI.Show();
        configuratorEditor.Hide();
        savePresetDialog.Hide();
    }

    private void OnDestroy()
    {
        presetSelectionUI.OnLoadPreset -= PresetLoader_OnLoadPreset;
        presetSelectionUI.OnCreateConfiguration -= PresetLoader_OnCreateConfiguration;
    }

    private void ChangeToPresetSelection()
    {
        configuratorEditor.Hide();
        presetSelectionUI.Show();
    }

    #region Subscriptions
    
    private void ChangeToConfiguratorEditor(CarConfigurationModel carModel = null)
    {
        configuratorEditor.SetConfigurationModel(carModel);
        presetSelectionUI.Hide();
        configuratorEditor.Show();
    }

    private void PresetLoader_OnCreateConfiguration(object sender, EventArgs e)
    {
        ChangeToConfiguratorEditor();
    }

    private void PresetLoader_OnLoadPreset(object sender, PresetSelectionUI.OnLoadPresetEventArgs e)
    {
        configuratorEditor.SetConfigurationEditingIndex(e.PresetIndex);
        ChangeToConfiguratorEditor(_persistentData.GetCarConfiguration(e.PresetIndex));
    }

    private void ConfigurationEditor_OnSaved(object sender, ConfigurationEditorUI.OnSavePressedEventArgs e)
    {
        savePresetDialog.Show((x) =>
        {
            var model = e.ModelToSave;
            model.PresetName = x;

            // Index less then zero means that we not editing existing model
            if (e.IndexToUpdate < 0)
                _persistentData.AddCarConfiguration(model);
            else
                _persistentData.UpdateCarConfiguration(model, e.IndexToUpdate);

            _persistentData.Save();

            ChangeToPresetSelection();
        });
    }
    
    private void ConfigurationEditor_OnBackPressed(object sender, EventArgs e)
    {
        // Show dialogue and set callback to go back
    }
    
    #endregion
}
