using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class CarConfigurationModel
{
    [SerializeField, JsonProperty] private int versionInfoId;
    [SerializeField, JsonProperty] private int engineInfoId;
    [SerializeField, JsonProperty] private int colorInfoId;
    [SerializeField, JsonProperty] private int upholsteryInfoId;
    [SerializeField, JsonProperty] private List<int> additionalPackageInfoIds = new List<int>();

    [JsonIgnore] public int VersionInfoId => versionInfoId;
    [JsonIgnore] public int EngineInfoId => engineInfoId;
    [JsonIgnore] public int ColorInfoId => colorInfoId;
    [JsonIgnore] public int UpholsteryInfoId => upholsteryInfoId;
    [JsonIgnore] public int[] AdditionalPackageInfoIds => additionalPackageInfoIds.ToArray();

    public void SetVersion(VersionInfoSO version)
    {
        versionInfoId = version == null ? 0 : version.GetInstanceID();
    }
    
    public void SetEngine(EngineInfoSO engine)
    {
        engineInfoId = engine == null ? 0 : engine.GetInstanceID();
    }

    public void SetColor(ColorInfoSO color)
    {
        colorInfoId = color == null ? 0 : color.GetInstanceID();
    }

    public void SetUpholstery(UpholsteryInfoSO upholstery)
    {
        upholsteryInfoId = upholstery == null ? 0 : upholstery.GetInstanceID();
    }

    public void AddAdditionalPackageInfo(AdditionalPackageInfoSO additionalPackageInfo)
    {
        var id = additionalPackageInfo.GetInstanceID();
        if(additionalPackageInfoIds.Contains(id)) return;
        
        additionalPackageInfoIds.Add(additionalPackageInfo.GetInstanceID());
    }

    public void RemoveAdditionalPackageInfo(AdditionalPackageInfoSO additionalPackageInfo)
    {
        var id = additionalPackageInfo.GetInstanceID();
        if(!additionalPackageInfoIds.Contains(id)) return;

        additionalPackageInfoIds.Remove(id);
    }
}