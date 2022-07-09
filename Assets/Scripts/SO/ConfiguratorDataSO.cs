using UnityEngine;

[CreateAssetMenu(fileName = "Configurator Data", menuName = "Data/Configurator Data", order = 0)]
public class ConfiguratorDataSO : ScriptableObject
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