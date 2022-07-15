using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        UpdateDropdown();

        dropdown.onValueChanged.AddListener(Dropdown_OnValueChanged);
        loadButton.onClick.AddListener(Load_OnClick);
        
        loadButton.interactable = false;
    }

    private void OnDestroy()
    {
        dropdown.onValueChanged.RemoveListener(Dropdown_OnValueChanged);
        loadButton.onClick.RemoveListener(Load_OnClick);
    }

    public void UpdateDropdown()
    {
        var dropList = new List<TMP_Dropdown.OptionData>();
        dropList.Add(new TMP_Dropdown.OptionData("Select"));
        // Load items from save data
        dropList.AddRange(
            GlobalObjects.GetPersistentData().CarConfigurations.Select(x =>
                new TMP_Dropdown.OptionData(x.PresetName)
            ));

        dropdown.options = dropList;
    }

    private void Dropdown_OnValueChanged(int index)
    {
        // First option always is a placeholder with "Select" text. Resetting values
        if (dropdown.options.Count < 2 || index == 0)
        {
            loadButton.interactable = false;
            _selectedDataIndex = -1;
            return;
        }

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
