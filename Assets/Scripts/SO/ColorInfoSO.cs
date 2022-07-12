using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Info", menuName = "Data/Color Info", order = 0)]
public class ColorInfoSO : ScriptableObject
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private bool isOption;
    [SerializeField] private VersionInfoSO[] compatibleVersions;

    public Color Color => color;
    public bool IsOption => isOption;
    public VersionInfoSO[] CompatibleVersions => compatibleVersions;
}