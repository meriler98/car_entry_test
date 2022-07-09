using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EngineInfo", menuName = "Data/Engine Info", order = 0)]
public class EngineInfoSO : ScriptableObject
{
    [SerializeField] private string engineName;
    [SerializeField] private VersionInfoSO[] compatibleVersions;
    
    public string EngineName => engineName;
    public VersionInfoSO[] CompatibleVersions => compatibleVersions;
}