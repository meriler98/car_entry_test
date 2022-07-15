using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfiguratorUI : MonoBehaviour
{
    [SerializeField] private PresetSelectionUI presetSelectionUI;
    [SerializeField] private ConfigurationEditorUI configuratorEditor;
    [SerializeField] private SavePresetDialog savePresetDialog;
    [SerializeField] private ConfirmationDialog confirmationDialog;


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
        confirmationDialog.Hide();
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
        var model = e.ModelToSave;

        if (e.IndexToUpdate < 0) {

            savePresetDialog.Show((x) =>
            {
                model.PresetName = x;
                _persistentData.AddCarConfiguration(model);

                ChangeToPresetSelection();
            });
        }
        else
        {
            _persistentData.UpdateCarConfiguration(model, e.IndexToUpdate);
            ChangeToPresetSelection();
        }
        _persistentData.Save();
    }

    
    private void ConfigurationEditor_OnBackPressed(object sender, EventArgs e)
    {
        confirmationDialog.Show(ChangeToPresetSelection);
    }
    
    #endregion
}
