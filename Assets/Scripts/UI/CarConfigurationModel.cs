using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class CarConfigurationModel
{
    [SerializeField, JsonProperty] public string PresetName { get; set; }
    [SerializeField, JsonProperty] public string VersionInfoId { get; private set; }
    [SerializeField, JsonProperty] public string EngineInfoId { get; private set; }
    [SerializeField, JsonProperty] public string ColorInfoId { get; private set; }
    [SerializeField, JsonProperty] public string UpholsteryInfoId { get; private set; }
    [SerializeField, JsonProperty] public List<string> AdditionalPackageInfoIds { get; private set; }

    public void SetVersion(VersionInfoSO version)
    {
        VersionInfoId = version == null ? "" : version.Guid;
    }
    
    public void SetEngine(EngineInfoSO engine)
    {
        EngineInfoId = engine == null ? "" : engine.Guid;
    }

    public void SetColor(ColorInfoSO color)
    {
        ColorInfoId = color == null ? "" : color.Guid;
    }

    public void SetUpholstery(UpholsteryInfoSO upholstery)
    {
        UpholsteryInfoId = upholstery == null ? "" : upholstery.Guid;
    }

    public void SetAdditionalPackageInfos(AdditionalPackageInfoSO[] additionalPackages)
    {
        AdditionalPackageInfoIds = additionalPackages.Select(x => x.Guid).ToList();
    }

    public void AddAdditionalPackageInfo(AdditionalPackageInfoSO additionalPackageInfo)
    {
        var id = additionalPackageInfo.Guid;
        if(AdditionalPackageInfoIds.Contains(id)) return;
        
        AdditionalPackageInfoIds.Add(additionalPackageInfo.Guid);
    }

    public void RemoveAdditionalPackageInfo(AdditionalPackageInfoSO additionalPackageInfo)
    {
        var id = additionalPackageInfo.Guid;
        if(!AdditionalPackageInfoIds.Contains(id)) return;

        AdditionalPackageInfoIds.Remove(id);
    }
}