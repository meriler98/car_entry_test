using UnityEngine;

[CreateAssetMenu(fileName = "Additional Package Info", menuName = "Data/Additional Package Info", order = 0)]
public class AdditionalPackageInfoSO : ScriptableObject
{
    [SerializeField, HideInInspector] private string guid;
    [SerializeField] private string packageName;
    [SerializeField] private VersionInfoSO[] compatibleVersions;

    public string PackageName => packageName;
    public VersionInfoSO[] CompatibleVersions => compatibleVersions;
    public string Guid => guid;

#if UNITY_EDITOR
    [ContextMenu("Regenerate Guid")]
    private void RegenerateGuid()
    {
        guid = System.Guid.NewGuid().ToString();
    }

    private void OnValidate()
    {
        if (guid == "")
            RegenerateGuid();
    }
#endif

}