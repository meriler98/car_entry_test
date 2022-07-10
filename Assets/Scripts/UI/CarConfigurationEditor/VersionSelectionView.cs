using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VersionSelectionView : MonoBehaviour
{
    [SerializeField] private ToggleButtonUI toggleTemplate;
    [SerializeField] private HorizontalLayoutGroup buttonContainer;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private ToggleGroupUI toggleGroup;

    private RectTransform _cachedRectTranform;
    private VersionInfoSO[] _versions;
    private Dictionary<ToggleButtonUI, VersionInfoSO> _versionInfoDirctionary = new Dictionary<ToggleButtonUI, VersionInfoSO>();

    public EventHandler<OnVersionInfoSelectedEventArgs> OnVersionInfoSelected;

    private void Awake()
    {
        _cachedRectTranform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        toggleGroup.OnToggleButtonSelected += ToggleGroup_OnToggleButtonSelected;
    }

    private void OnDestroy()
    {
        toggleGroup.OnToggleButtonSelected += ToggleGroup_OnToggleButtonSelected;
    }

    public void SetVersions(params VersionInfoSO[] versionInfos)
    {
        _versions = versionInfos;

        UpdateToggles();
    }

    public void SetToggleSelectionByVersionId(int versionId, bool isToggleOn)
    {
        var toggleForVersion = _versionInfoDirctionary
            .FirstOrDefault(x => x.Value.GetInstanceID() == versionId)
            .Key;

        if (toggleForVersion == null)
        {
            Debug.LogError("Toggle with such Version Info ID was not found");
            return;
        }

        UpdateVersionInfo(toggleForVersion, isToggleOn);
    }

    private void UpdateVersionInfo(ToggleButtonUI toggle, bool isToggledOn)
    {
        var versionInfo = _versionInfoDirctionary[toggle];
                
        if (versionInfo == null)
        {
            Debug.LogError("Couldn't find toggle stored in dictionary!");
        }
                
        if (isToggledOn)
        {
            OnVersionInfoSelected?.Invoke(this, new OnVersionInfoSelectedEventArgs(versionInfo));
            descriptionText.text = versionInfo.Description;
        }
        else
            descriptionText.text = "";

        // Force rebuild UI for content fitter
        UIUtilities.ForceLayoutRebuild(this, _cachedRectTranform);
    }

    private void UpdateToggles()
    {
        // Removing old toggles
        toggleGroup.ClearToggles();
        
        _versionInfoDirctionary.Clear();

        // Replacing with new ones
        foreach (var version in _versions)
        {
            var toggle = Instantiate(toggleTemplate, buttonContainer.transform);
            
            toggle.SetButtonText(version.VersionName);
            toggleGroup.AddToggleToGroup(toggle);

            _versionInfoDirctionary[toggle] = version;
        }
    }

    private void ToggleGroup_OnToggleButtonSelected(object sender, ToggleGroupUI.OnToggleSelectedEventArgs e)
    {
        var toggleVersionPair = _versionInfoDirctionary
            .FirstOrDefault(x => x.Key == e.SelectedToggle);

        if (toggleVersionPair.Value == null)
        {
            Debug.LogError("Failed to find corresponding version info");
            return;
        }
        
        UpdateVersionInfo(e.SelectedToggle, e.IsToggledOn);
        
        if(e.IsToggledOn)
            OnVersionInfoSelected?.Invoke(this, new OnVersionInfoSelectedEventArgs(toggleVersionPair.Value));
    }

    public class OnVersionInfoSelectedEventArgs : EventArgs
    {
        public VersionInfoSO VersionSelected { get; }

        public OnVersionInfoSelectedEventArgs(VersionInfoSO versionSelected)
        {
            VersionSelected = versionSelected;
        }
    }
}


