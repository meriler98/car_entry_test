using UnityEngine;

[CreateAssetMenu(fileName = "Upholstery Info", menuName = "Data/Upholstery Info", order = 0)]
public class UpholsteryInfoSO : ScriptableObject
{
    [SerializeField, HideInInspector] private string guid;
    [SerializeField] private Color color = Color.white;
    [SerializeField] private VersionInfoSO[] compatibleVersions;

    public Color Color => color;
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