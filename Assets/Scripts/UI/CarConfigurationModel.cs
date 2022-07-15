using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class CarConfigurationModel
{
    [SerializeField, JsonProperty] public string PresetName { get; set; }
    [SerializeField, JsonProperty] public int VersionInfoId { get; private set; }
    [SerializeField, JsonProperty] public int EngineInfoId { get; private set; }
    [SerializeField, JsonProperty] public int ColorInfoId { get; private set; }
    [SerializeField, JsonProperty] public int UpholsteryInfoId { get; private set; }
    [SerializeField, JsonProperty] public List<int> AdditionalPackageInfoIds { get; private set; }

    public void SetVersion(VersionInfoSO version)
    {
        VersionInfoId = version == null ? 0 : version.GetInstanceID();
    }
    
    public void SetEngine(EngineInfoSO engine)
    {
        EngineInfoId = engine == null ? 0 : engine.GetInstanceID();
    }

    public void SetColor(ColorInfoSO color)
    {
        ColorInfoId = color == null ? 0 : color.GetInstanceID();
    }

    public void SetUpholstery(UpholsteryInfoSO upholstery)
    {
        UpholsteryInfoId = upholstery == null ? 0 : upholstery.GetInstanceID();
    }

    public void SetAdditionalPackageInfos(AdditionalPackageInfoSO[] additionalPackages)
    {
        AdditionalPackageInfoIds = additionalPackages.Select(x => x.GetInstanceID()).ToList();
    }

    public void AddAdditionalPackageInfo(AdditionalPackageInfoSO additionalPackageInfo)
    {
        var id = additionalPackageInfo.GetInstanceID();
        if(AdditionalPackageInfoIds.Contains(id)) return;
        
        AdditionalPackageInfoIds.Add(additionalPackageInfo.GetInstanceID());
    }

    public void RemoveAdditionalPackageInfo(AdditionalPackageInfoSO additionalPackageInfo)
    {
        var id = additionalPackageInfo.GetInstanceID();
        if(!AdditionalPackageInfoIds.Contains(id)) return;

        AdditionalPackageInfoIds.Remove(id);
    }
}