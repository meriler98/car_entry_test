using UnityEngine;

[CreateAssetMenu(fileName = "Upholstery Info", menuName = "Data/Upholstery Info", order = 0)]
public class UpholsteryInfoSO : ScriptableObject
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private VersionInfoSO[] compatibleVersions;

    public Color Color => color;
    public VersionInfoSO[] CompatibleVersions => compatibleVersions;
}