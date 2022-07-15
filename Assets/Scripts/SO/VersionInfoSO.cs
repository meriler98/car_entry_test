using Data;
using UnityEngine;

[CreateAssetMenu(fileName = "Version Info", menuName = "Data/Version Info", order = 0)]
public class VersionInfoSO : ScriptableObject
{
    [SerializeField, HideInInspector] private string guid;
    [SerializeField] private string versionName;
    [SerializeField, TextArea] private string description;

    public string VersionName => versionName;
    public string Description => description;
    public string Guid => guid;

#if UNITY_EDITOR
    [ContextMenu("Regenerate Guid")]
    private void RegenerateGuid()
    {
        guid = System.Guid.NewGuid().ToString();
    }

    private void OnValidate()
    {
        if(guid == "")
           RegenerateGuid();
    }
#endif
}