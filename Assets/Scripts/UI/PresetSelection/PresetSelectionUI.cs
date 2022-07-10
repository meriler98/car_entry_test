using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresetSelectionUI : MonoBehaviour
{
    [SerializeField] private ConfigurationLoaderView configurationLoaderView;
    [SerializeField] private Button startCreationButton;
    [SerializeField] private ConfigurationEditorUI configurationEditorUI;
    
    private PersistentData _persistentData;

    private void Start()
    {
        InitializePersistentData();
        
        configurationLoaderView.OnModelLoad += Loader_OnModelLoad;
        startCreationButton.onClick.AddListener(Start_OnClick);
    }

    private void OnDestroy()
    {
        configurationLoaderView.OnModelLoad -= Loader_OnModelLoad;
        startCreationButton.onClick.RemoveListener(Start_OnClick);
    }

    private void InitializePersistentData()
    {
        _persistentData = FindObjectOfType<PersistentData>();

        if (_persistentData == null)
        {
            Debug.LogWarning("Persistent data object is missing! Creating new");
            var newGo = new GameObject("PersistentData");
            _persistentData = newGo.AddComponent<PersistentData>();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SwitchToConfigurationEditor(int editingIndex)
    {
        configurationEditorUI.SetConfigurationModel(_persistentData.GetCarConfiguration(editingIndex));
        configurationEditorUI.SetConfigurationEditingIndex(editingIndex);
        
        Hide();
        configurationEditorUI.Show();
    }
    
    public void SwitchToConfigurationEditor()
    {
        configurationEditorUI.SetConfigurationModel(new CarConfigurationModel());
        
        Hide();
        configurationEditorUI.Show();
    }
    
    private void Loader_OnModelLoad(object sender, ConfigurationLoaderView.OnModelLoadedEventArgs e)
    {
        SwitchToConfigurationEditor(e.LoadedIndex);
    }

    private void Start_OnClick()
    {
        SwitchToConfigurationEditor();
    }
}
