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

    public EventHandler<OnLoadPresetEventArgs> OnLoadPreset;
    public EventHandler OnCreateConfiguration;

    private void Start()
    {
        configurationLoaderView.OnModelLoad += Loader_OnModelLoad;
        startCreationButton.onClick.AddListener(Start_OnClick);
    }

    private void OnDestroy()
    {
        configurationLoaderView.OnModelLoad -= Loader_OnModelLoad;
        startCreationButton.onClick.RemoveListener(Start_OnClick);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Loader_OnModelLoad(object sender, ConfigurationLoaderView.OnModelLoadedEventArgs e)
    {
        OnLoadPreset?.Invoke(this, new OnLoadPresetEventArgs(e.LoadedIndex));
    }

    private void Start_OnClick()
    {
        OnCreateConfiguration?.Invoke(this, EventArgs.Empty);
    }

    public class OnLoadPresetEventArgs : EventArgs
    {
        public int PresetIndex { get; }

        public OnLoadPresetEventArgs(int presetIndex)
        {
            PresetIndex = presetIndex;
        }
    }
}
