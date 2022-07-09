using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Info", menuName = "Data/Color Info", order = 0)]
public class ColorInfoSO : ScriptableObject
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private VersionCompatibilityData[] compatibleVersions;

    public Color Color => color;
    public VersionCompatibilityData[] CompatibleVersions => compatibleVersions;
}