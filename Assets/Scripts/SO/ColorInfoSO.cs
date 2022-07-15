using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Info", menuName = "Data/Color Info", order = 0)]
public class ColorInfoSO : ScriptableObject
{
    [SerializeField, HideInInspector] private string guid;
    [SerializeField] private Color color = Color.white;
    [SerializeField] private bool isOption;
    [SerializeField] private VersionInfoSO[] compatibleVersions;

    public Color Color => color;
    public bool IsOption => isOption;
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