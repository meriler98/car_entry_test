using Data;
using UnityEngine;

[CreateAssetMenu(fileName = "Version Info", menuName = "Data/Version Info", order = 0)]
public class VersionInfoSO : ScriptableObject
{
    [SerializeField] private string versionName;
    [SerializeField, TextArea] private string description;

    public string VersionName => versionName;
    public string Description => description;
}