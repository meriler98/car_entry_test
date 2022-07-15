using UnityEngine;

[CreateAssetMenu(fileName = "ConfiguratorDataLookup", menuName = "Data/Configurator Data Lookup", order = 0)]
public class ConfiguratorDataLookupSO : ScriptableObject
{
    [SerializeField] private VersionInfoSO[] versionInfos;
    [SerializeField] private EngineInfoSO[] engineInfos;
    [SerializeField] private ColorInfoSO[] colorInfos;
    [SerializeField] private UpholsteryInfoSO[] upholsteryInfos;
    [SerializeField] private AdditionalPackageInfoSO[] additionalPackageInfos;

    public VersionInfoSO[] VersionInfos => versionInfos;
    public EngineInfoSO[] EngineInfos => engineInfos;
    public ColorInfoSO[] ColorInfos => colorInfos;
    public UpholsteryInfoSO[] UpholsteryInfos => upholsteryInfos;
    public AdditionalPackageInfoSO[] AdditionalPackageInfos => additionalPackageInfos;
}