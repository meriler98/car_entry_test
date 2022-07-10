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
    [SerializeField] private ContentSizeFitter buttonContainerContentSizeFitter;
    [SerializeField] private TMP_Text descriptionText;

    private RectTransform _cachedRectTranform;
    private VersionInfoSO[] _versions;
    private List<ToggleButtonUI> _toggles = new List<ToggleButtonUI>();
    private Dictionary<ToggleButtonUI, VersionInfoSO> _versionInfoDirctionary = new Dictionary<ToggleButtonUI, VersionInfoSO>();

    public EventHandler<OnVersionInfoSelectedEventArgs> OnVersionInfoSelected;

    private void Awake()
    {
        _cachedRectTranform = GetComponent<RectTransform>();
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

        SetToggleSelected(toggleForVersion, isToggleOn);
    }

    private void SetToggleSelected(ToggleButtonUI toggle, bool isToggledOn)
    {
        foreach (var toggleElement in _toggles)
        {
            if (toggleElement.GetInstanceID() == toggle.GetInstanceID())
            {
                var versionInfo = _versionInfoDirctionary[toggleElement];
                
                if (versionInfo == null)
                {
                    Debug.LogError("Couldn't find toggle stored in dictionary!");
                    continue;
                }

                // Updating description field
                if (isToggledOn)
                {
                    OnVersionInfoSelected?.Invoke(this, new OnVersionInfoSelectedEventArgs(versionInfo));
                    descriptionText.text = versionInfo.Description;
                }
                else
                    descriptionText.text = "";
                
                continue;
            };
            
            toggleElement.SetToggle(false);
        }

        // Force rebuild UI for content fitter
        UIUtilities.ForceLayoutRebuild(this, _cachedRectTranform);
    }
    
    private void UpdateToggles()
    {
        // Removing old toggles
        foreach (var toggle in _toggles)
        {
            toggle.OnToggled -= Toggle_OnToggled;
            Destroy(toggle.gameObject);
        }
        _toggles.Clear();
        _versionInfoDirctionary.Clear();

        // Replacing with new ones
        foreach (var version in _versions)
        {
            var toggle = Instantiate(toggleTemplate, buttonContainer.transform);
            
            toggle.SetButtonText(version.VersionName);
            toggle.OnToggled += Toggle_OnToggled;
            
            _toggles.Add(toggle);
            _versionInfoDirctionary[toggle] = version;
        }
    }

    private void Toggle_OnToggled(object sender, ToggleButtonUI.OnToggledEventArgs e)
    {
        var toggle = _toggles.FirstOrDefault(x => e.InstanceID == x.GetInstanceID());
        
        if (toggle == null)
        {
            Debug.LogError($"Couldn't find toggle with ID {e.InstanceID}");
            return;
        }
        
        SetToggleSelected(toggle, e.ToggledOn);
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


