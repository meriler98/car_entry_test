using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationLoaderView : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Button loadButton;

    private int _selectedDataIndex = -1;

    public EventHandler<OnModelLoadedEventArgs> OnModelLoad;

    private void Start()
    {
        InitializeDropdown();
        loadButton.interactable = false;
        loadButton.onClick.AddListener(Load_OnClick);
    }

    private void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(Dropdown_OnValueChanged);
        loadButton.onClick.RemoveListener(Load_OnClick);
    }

    private void InitializeDropdown()
    {
        var dropList = new List<TMP_Dropdown.OptionData>();
        dropList.Add(new TMP_Dropdown.OptionData("Select"));
        // Load items from save data
        dropList.Add(new TMP_Dropdown.OptionData("My config 1"));

        dropdown.options = dropList; 

        dropdown.onValueChanged.AddListener(Dropdown_OnValueChanged);
    }

    private void Dropdown_OnValueChanged(int index)
    {
        // First option always is a placeholder with "Select" text. Ignoring it
        if(dropdown.options.Count < 2 && index > 0) return;

        _selectedDataIndex = index - 1;
        loadButton.interactable = true;
    }

    private void Load_OnClick()
    {
        OnModelLoad?.Invoke(this, new OnModelLoadedEventArgs(_selectedDataIndex));
    }

    public class OnModelLoadedEventArgs : EventArgs
    {
        public int LoadedIndex { get; }

        public OnModelLoadedEventArgs(int loadedIndex)
        {
            LoadedIndex = loadedIndex;
        }
    }
}
