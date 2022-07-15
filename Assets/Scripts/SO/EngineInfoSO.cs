using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EngineInfo", menuName = "Data/Engine Info", order = 0)]
public class EngineInfoSO : ScriptableObject
{
    [SerializeField, HideInInspector] private string guid;
    [SerializeField] private string engineName;
    [SerializeField] private VersionInfoSO[] compatibleVersions;
    
    public string EngineName => engineName;
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