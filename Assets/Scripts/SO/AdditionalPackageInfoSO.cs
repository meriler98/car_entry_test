using UnityEngine;

[CreateAssetMenu(fileName = "Additional Package Info", menuName = "Data/Additional Package Info", order = 0)]
public class AdditionalPackageInfoSO : ScriptableObject
{
    [SerializeField] private string packageName;
    [SerializeField] private VersionCompatibilityData[] compatibleVersions;

    public string PackageName => packageName;
    public VersionCompatibilityData[] CompatibleVersions => compatibleVersions;

}